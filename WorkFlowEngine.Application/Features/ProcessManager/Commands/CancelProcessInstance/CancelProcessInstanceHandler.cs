using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkFlowEngine.Application.Abstractions.ProcessManager;
using WorkFlowEngine.Domain.Entities;
using WorkFlowEngine.Infrastructure.Abstraction;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Command.CancelProcessInstance
{
    public class CancelProcessInstanceHandler : BaseCommandHandler, IRequestHandler<CancelProcessInstanceRequest,
Result<bool>>
    {
        public CancelProcessInstanceHandler(IMapper mapper, IProcessMangerQueryRepository queryRepo, IProcessMangerCommandReposistory commandQuery, IDataCash dataCash, IInstanceAccess instanceAccess) : base(commandQuery, mapper, queryRepo, dataCash, instanceAccess)
        {
        }

        async Task<Result<bool>> IRequestHandler<CancelProcessInstanceRequest, Result<bool>>.Handle(CancelProcessInstanceRequest request, CancellationToken cancellationToken)
        {


            var cancelAction = await _queryRepo.GetAsync<Domain.Entities.Action>(
               condition: s => s.Name == Constants.Actions.Cancel,
               asNoTracking: true);

            if (cancelAction is null)
                return new Result<bool>(false, new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>(nameof(Constants.Error.NoCancelAction), Constants.Error.NoCancelAction) });

            var instance = await _queryRepo.GetAsync<ProcessInstance>(
                condition: s => s.Id == request.ProcessInstanceId,
                includes: source =>
                    source.Include(s => s.ProcessInstanceHistories).
                    Include(s => s.ProcessInstanceUsers),
                false);
            if (instance is null)
                return new Result<bool>(false, new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>(nameof(Constants.Error.NotFound), Constants.Error.NotFound) });

            //ToDo:Assure that the canceled by has the authority to cancel Instance.
            var userGroup = _dataCash.GetUserGroups(request.UserId);
            var hasAccess = _instanceAccess.HasInstanceAccess(instance, request.UserId, userGroup);
            if (!hasAccess.IsSuccess)
                return new Result<bool>(false, hasAccess.Errors);
            if (!hasAccess.Data)
                return new Result<bool>(false, new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>(Constants.Error.NotAuthorized, Constants.Error.NotAuthorized)
                });
            var process = await _dataCash.GetProcessById(instance.ProcessId);

            instance.SetProcess(process);

            var result = instance.Cancel(cancelAction, request.UserId);
            if (!result.IsSuccess)
                return new Result<bool>(false, result.Errors);

            _commandRepo.Update<ProcessInstance>(instance);
            return new Result<bool>(true, true);
        }
    }
}
