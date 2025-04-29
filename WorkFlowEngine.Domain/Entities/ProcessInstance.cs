using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using WorkFlowEngine.Domain.Enums;
using WorkFlowEngine.Domain.Primitives;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Domain.Entities
{
    public class ProcessInstance : Entity
    {
        public int ProcessId { get; private set; }
        [MaxLength(50)]
        public string ApplicationNumber { get; private set; } = string.Empty;
        [MaxLength(50)]
        public string? ApplicantNumber { get; private set; }
        public virtual Process Process { get; private set; }
        public int CurrentActivityId { get; private set; }
        public virtual Activity CurrentActivity { get; private set; }
        public DateTime CreationDate { get; private set; }
        [MaxLength(500)]
        public string TaskUrl { get; private set; } = string.Empty;
        public bool Opened { get; private set; }
        [MaxLength(150)]
        public string? LastModifiedBy { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime LastModificationDate { get; private set; }
        public virtual ICollection<ProcessInstanceDataField> ProcessInstanceDataFields { get; private set; }
        public virtual ICollection<ProcessInstanceUser> ProcessInstanceUsers { get; private set; }
        public virtual ICollection<ProcessInstanceHistory> ProcessInstanceHistories { get; private set; }
        private ProcessInstance()
        {
            ProcessInstanceDataFields = new List<ProcessInstanceDataField>();
            ProcessInstanceUsers = new List<ProcessInstanceUser>();
            ProcessInstanceHistories = new List<ProcessInstanceHistory>();
        }

        public ProcessInstance(Process process) : this()
        {
            Process = process;
        }
        public void SetProcess(Process process) => Process = process;
        public Result<ProcessInstance> Start(Dictionary<string, string> dataFields, string userId, string applicationNumber, string applicantNumber)
        {
            var result = ValidateConfiguration(dataFields);
            if (!result.IsSuccess)
                return result;

            IsActive = true;
            Opened = false;
            CreationDate = DateTime.Now;
            LastModificationDate = DateTime.Now;
            ApplicationNumber = applicationNumber;
            LastModifiedBy = userId;
            ApplicantNumber = applicantNumber;
            TaskUrl = dataFields.GetValueOrDefault(DataFieldName.ServiceURL.ToString())!;
            CurrentActivity = Process.getStartActivity()!;
            AddDataFields(dataFields);
            return ExecuteActivityAction(userId, Constants.Actions.Execute, null!);
        }
        public Result<ProcessInstance> Cancel(Action cancelAction, string canceledBy)
        {
            if (!IsActive)
                return ReturnError<ProcessInstance>(new KeyValuePair<string, string>(nameof(canceledBy), Constants.Error.ClosedProcess));
            var endActivity = Process.getEndActivity();
            if (endActivity is null)
                return ReturnError<ProcessInstance>(new KeyValuePair<string, string>(nameof(Constants.Error.NoEndActivity), Constants.Error.NoEndActivity));

            IsActive = false;
            LastModifiedBy = canceledBy;
            ProcessInstanceHistories.Add(new ProcessInstanceHistory(CurrentActivityId, cancelAction.Id, endActivity.Id, canceledBy));
            return new Result<ProcessInstance>(this, true);
        }
        public Result<ProcessInstance> Release(Action releaseAction, string releaseBy)
        {
            if (!IsActive)
                return ReturnError<ProcessInstance>(new KeyValuePair<string, string>(nameof(Constants.Error.ClosedProcess), Constants.Error.ClosedProcess));

            Opened = false;
            LastModifiedBy = releaseBy;
            ProcessInstanceHistories.Add(new ProcessInstanceHistory(CurrentActivityId, releaseAction.Id, CurrentActivityId, releaseBy));
            return new Result<ProcessInstance>(this, true);
        }
        public Result<ProcessInstance> ExecuteActivityAction(string userId, string action, Dictionary<string, string> dataFields = null!)
        {
            var actionValidation = ValidateAction(action);
            if (!actionValidation.IsSuccess)
                return ReturnError<ProcessInstance>(actionValidation.Errors.FirstOrDefault());
            if (dataFields != null)
                UpdateDataFields(dataFields);

            var actionId = actionValidation.Data;
            Activity nextActivity;

            if (IsPerDestination(userId))
            {
                nextActivity = CurrentActivity;
                ProcessInstanceHistories.Add(new ProcessInstanceHistory(CurrentActivity.Id, actionId, nextActivity.Id, userId));
                DeactivateProcessInstanceUser(userId);
            }
            else
            {
                var result = Transit(actionId);
                if (!result.IsSuccess)
                    return new Result<ProcessInstance>(false, result.Errors);
                nextActivity = result.Data;
                ProcessInstanceHistories.Add(new ProcessInstanceHistory(CurrentActivity.Id, actionId, nextActivity.Id, userId));
                CurrentActivity = nextActivity;
                DeactivateCurrentUsers();
                AddProcessInstanceUser();
            }



            LastModificationDate = DateTime.Now;
            LastModifiedBy = userId;
            Opened = false;

            if (nextActivity.Name == Constants.Activities.EndActivity)
            {
                IsActive = false;
                return new Result<ProcessInstance>(this, true);
            }

            //if (nextActivity.ActivityType == ActivityType.DataActivity)
            //{
            //    var _nextActivity = SetActivityDataFields(actionId);
            //    if (!_nextActivity.IsSuccess)
            //        return ReturnError<ProcessInstance>(actionValidation.Errors.FirstOrDefault());
            //    nextActivity = _nextActivity.Value;
            //}

            if (nextActivity.ActivityType == ActivityType.ServerActivity || nextActivity.ActivityType == ActivityType.DataActivity)
                return ExecuteActivityAction(Constants.Roles.ProcessManager, Constants.Actions.Execute, dataFields!);

            return new Result<ProcessInstance>(this, true);
        }
        private bool IsPerDestination(string userId)
        => CurrentActivity.RuleOption == RuleOption.PerDestination && ProcessInstanceUsers.Any(s => s.DestinationName != userId && s.IsActive);


        private Result<int> SetActivityDataFields(Transition transition)
        {
            foreach (var condition in transition.TransitionConditions)
            {
                var _dataField = ProcessInstanceDataFields.FirstOrDefault(s => s.DataFieldId == condition.DataFieldId);
                if (_dataField is null)
                    return ReturnError<int>(
                new KeyValuePair<string, string>($"Data Field Id:{condition.DataFieldId} {condition.DataField?.Name}", Constants.Error.DataFieldIsMissing));

                if (condition.Operator == Operator.Set)
                    _dataField.SetDataValue(condition.Value);
                // ToDo add more operation like plus, minus, multiply, etc...
            }
            return new Result<int>(transition.NextActivityId, true);
        }
        private Result<int> ValidateAction(string action)
        {
            if (CurrentActivity is null)
                return ReturnError<int>(
           new KeyValuePair<string, string>(action, Constants.Error.NoNextActivity));

            if (string.IsNullOrEmpty(action))
                return ReturnError<int>(
           new KeyValuePair<string, string>(action, Constants.Error.NotNull));
            var actionId = CurrentActivity.ActivityActions.Where(s => s.Action.Name == action).Select(s => s.ActionId).FirstOrDefault();
            if (actionId == 0)
                return ReturnError<int>(
           new KeyValuePair<string, string>(action, Constants.Error.ActionNotAllowed));
            return new Result<int>(actionId, true);
        }
        private void UpdateDataFields(Dictionary<string, string> dataFields)
        {
            foreach (var item in dataFields)
            {
                var dataField = ProcessInstanceDataFields.FirstOrDefault(s => s.DataField.Name == item.Key);
                if (dataField is not null)
                    dataField.SetDataValue(item.Value);
            }
        }
        private void AddDataFields(Dictionary<string, string> dataFields)
        {
            foreach (var dataField in Process.ProcessDataFields)
            {
                ProcessInstanceDataFields.Add(new ProcessInstanceDataField
                (
                   dataField.DataField, dataFields.GetValueOrDefault(dataField.DataField.Name) ?? dataField.DefaultValue
                ));
            }
        }
        private Result<ProcessInstance> ValidateConfiguration(Dictionary<string, string> dataFields)
        {
            if (Process is null)
                return ReturnError<ProcessInstance>(
                   new KeyValuePair<string, string>(nameof(Process), Constants.Error.NotNull));

            var startActivity = Process.getStartActivity();
            if (startActivity is null)
                return ReturnError<ProcessInstance>(
                       new KeyValuePair<string, string>(nameof(Constants.Error.NoStartActivity), Constants.Error.NoStartActivity));

            var requireDataFields = GetRequiredDataFields();

            if (requireDataFields.Any() && (dataFields is null || dataFields.Count == 0))
                return ReturnError<ProcessInstance>(
                         new KeyValuePair<string, string>(nameof(dataFields), Constants.Error.NotNull));


            foreach (var dataField in requireDataFields)
            {
                if (!dataFields.Any(s => s.Key == dataField.Name))
                    return ReturnError<ProcessInstance>(
                    new KeyValuePair<string, string>(dataField.Name, Constants.Error.IsMissing));
            }
            return new Result<ProcessInstance>(this, true);
        }
        private Expression<Func<TransitionConditions, bool>> ApplyConditions(Operator @operator, string value)
        {
            switch (@operator)
            {
                case Operator.Equal:
                    return s => s.Value == value;
                case Operator.NotEqual:
                    return s => s.Value != value;
                case Operator.LessThan:
                    return s => int.Parse(s.Value) < int.Parse(value);
                case Operator.GreaterThan:
                    return s => int.Parse(s.Value) > int.Parse(value);
                case Operator.LessThanOrEqual:
                    return s => int.Parse(s.Value) <= int.Parse(value);
                case Operator.GreaterThanOrEqual:
                    return s => int.Parse(s.Value) >= int.Parse(value);
                default:
                    return s => s.Value == value;
            }
        }
        private Result<T> ReturnError<T>(KeyValuePair<string, string> error)
            => new Result<T>(false, new List<KeyValuePair<string, string>> { error });
        private Result<Activity> Transit(int actionId)
        {

            var transitions = Process.Transitions.Where(s => s.CurrentActivityId == CurrentActivity.Id && s.ActionId == actionId).ToList();

            if (transitions is null || transitions.Count == 0)
                return ReturnError<Activity>(
                     new KeyValuePair<string, string>(nameof(Constants.Error.NoNextActivity), Constants.Error.NoNextActivity));
            int nextActivityId = 0;
            if (CurrentActivity.ActivityType == ActivityType.DataActivity)
            {
                var _setActivityResult = SetActivityDataFields(transitions.FirstOrDefault()!);
                if (!_setActivityResult.IsSuccess)
                    return new Result<Activity>(false, _setActivityResult.Errors);
                nextActivityId = _setActivityResult.Data;
            }
            else
            {
                foreach (var item in transitions)
                {
                    if (!item.TransitionConditions.Any())
                    {
                        nextActivityId = item.NextActivityId;
                        break;
                    }
                    //var predicate = PredicateBuilder.New<TransitionConditions>();
                    //predicate.And(s => s.TransitionId == item.Id); 
                    var matchCondition = false;
                    foreach (var condition in item.TransitionConditions)
                    {
                        var _instanceDataField = ProcessInstanceDataFields.FirstOrDefault(s => s.DataFieldId == condition.DataFieldId);
                        if (_instanceDataField is null)
                        {
                            return ReturnError<Activity>(
                        new KeyValuePair<string, string>(Constants.Keys.ProcessDataField, Constants.Error.IsMissing));
                        }
                        else
                        {
                            matchCondition = condition.MatchsCondition(_instanceDataField.DataFieldId, _instanceDataField.Value);
                            //.DataFieldId == _instanceDataField.DataFieldId && condition.Value == _instanceDataField.Value;
                            if (!matchCondition && condition.BinaryOperator == BinaryOperator.And)
                                break;
                            if (matchCondition && condition.BinaryOperator == BinaryOperator.Or)
                                break;
                            //if (condition.BinaryOperator == BinaryOperator.And)
                            //    predicate.And(ApplyConditions(condition.Operator, _dataField.Value));
                            //if (condition.BinaryOperator == BinaryOperator.Or)
                            //    predicate.Or(ApplyConditions(condition.Operator, _dataField.Value));
                        }
                    }
                    if (matchCondition)
                    {
                        nextActivityId = item.NextActivityId;
                        break;
                    }
                }
            }

            var nextActivity = Process.GetActivity(nextActivityId);
            if (nextActivity is null)
                return ReturnError<Activity>(new KeyValuePair<string, string>(nameof(Constants.Error.NoNextActivity), Constants.Error.NoNextActivity));

            return new Result<Activity>(nextActivity, true);
        }

        private bool MatchCondition()
        {
            return false;
        }
        private void DeactivateCurrentUsers()
        {
            foreach (var item in ProcessInstanceUsers)
                item.Deactivate();
        }
        private void AddProcessInstanceUser()
        {
            foreach (var item in CurrentActivity.ActivityDestinationTypes)
            {
                var destinationName = ProcessInstanceDataFields.Where(s => s.DataField.Id == item.ValueDataFieldId).FirstOrDefault()?.Value;
                if (destinationName is null)
                    continue;
                if (destinationName.Contains(";"))
                {
                    var destinations = destinationName.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    foreach (var destination in destinations)
                        ProcessInstanceUsers.Add(new ProcessInstanceUser(item.DestinationTypeID, destination));
                }
                else
                    ProcessInstanceUsers.Add(new ProcessInstanceUser(item.DestinationTypeID, destinationName));
            }
        }
        private void DeactivateProcessInstanceUser(string userId)
        {
            var user = ProcessInstanceUsers.FirstOrDefault(s => s.DestinationName == userId && s.IsActive);
            if (user is not null)
                user.Deactivate();
        }

        private List<DataField> GetTransition()
        {

            var result = new List<DataField>();

            return Process.ProcessDataFields.Where(s => s.IsRequired &&
            string.IsNullOrEmpty(s.DefaultValue)).Select(s => s.DataField).ToList();
        }
        private List<DataField> GetRequiredDataFields()
        {
            return Process.ProcessDataFields.Where(s => s.IsRequired
            && string.IsNullOrEmpty(s.DefaultValue)).Select(s => s.DataField).ToList();
        }
        public Result<string> OpenTask(string username)
        {
            if (Opened && LastModifiedBy?.ToLower() == username?.ToLower())
                return new Result<string>("Opened", true);
            if (Opened)
                return ReturnError<string>(new KeyValuePair<string, string>(nameof(Constants.Error.TaskOpened), Constants.Error.TaskOpened));

            Opened = true;
            LastModifiedBy = username;
            LastModificationDate = DateTime.Now;
            return new Result<string>("Opened", true);
        }
    }
}
