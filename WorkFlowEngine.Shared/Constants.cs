
namespace WorkFlowEngine.Shared
{
    public static class Constants
    {
        public static class Activities
        {
            public static readonly string StartActivity = "StartActivity";
            public static readonly string EndActivity = "EndActivity";
        }
        public static class Actions
        {
            public static readonly string Execute = "Execute";
            public static readonly string Release = "Release";
            public static readonly string Cancel = "Cancel";
        }
        public static class CashKeys
        {
            public static readonly string UserGroups = "userGroups_{0}";
            public static readonly string Process = "Process_{0}";
        }
        public static class CashExpirationInMinutes
        {
            public static readonly int OneMinute = 1;
            public static readonly int OneHour = OneMinute * 60;
            public static readonly int OneDay = OneHour * 24;
            public static readonly int OnWeek = OneDay * 7;
            public static readonly int OnMonth = OneDay * 365;
            public static readonly int OnYear = OnMonth * 12;
            public static readonly int Default = OneDay * 7;
        }
        public static class Roles
        {
            public static readonly string PowerUser = "PowerUsers";
            public static readonly string ProcessManager = "ProcessManager";
        }
        public static class Error
        {
            public static readonly string NoProcess = "Can not find any process with this name";
            public static readonly string NoResponse = "The Server doesn't return any response.";
            public static readonly string NotNull = "Can not be null or empty.";
            public static readonly string IsMissing = "Property is missing.";
            public static readonly string DataFieldIsMissing = "Can not find data field  to set.";
            public static readonly string NoNextActivity = "Can not find any configured activity.";
            public static readonly string NoNextDataActivity = "Can not find any configured data activity.";
            public static readonly string NoStartActivity = "Can not find configured start activity.";
            public static readonly string NoEndActivity = "Can not find configured end activity.";
            public static readonly string NoCancelAction = "Can not find configured cancel action.";
            public static readonly string NoReleaseAction = "Can not find configured release action.";
            public static readonly string ActionNotAllowed = "this action is not allowed for this activity.";
            public static readonly string NoAssignee = "The task assignee is empty.";
            public static readonly string TaskOpened = "The task already Opened.";
            public static readonly string NotAuthorized = "The user has no access to this task.";
            public static readonly string NotFound = "Can not found any data by this criteria.";
            public static readonly string ClosedProcess = "Can not take an action on closed process.";
            public static readonly string InvalidUsernameOrPassword = "Invalid username or password.";
        }
        public static class Keys
        {
            public static readonly string ProcessDataField = "ProcessDataField";

        }

    }
}
