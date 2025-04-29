namespace WorkFlowEngine.Api
{
    public static class RoutTable
    {
        private const string _apiPrefi = "";
        public class ProcessManager
        {
            public const string Controller = $"{_apiPrefi}/ProcessManger";
            public const string StartProcessInstance = "StartProcessInstance";
            public const string ExecuteActivityAction = "ExecuteActivityAction";
            public const string GetActivityActionNames = "GetActivityActionNames";
            public const string GetCurrentActivity = "GetCurrentActivity";
            public const string GetUserAvailableWorklist = "GetUserAvailableWorklist";
            public const string CancelProcessInstance = "CancelProcessInstance";
            public const string ReleaseTask = "ReleaseTask";
            public const string OpenTask = "OpenTask";
            public const string GetUserTaskInfo = "GetUserTaskInfo";
            public const string GetSerialNumber = "GetSerialNumber";
            public const string GetUserOpenedWorklist = "GetUserOpenedWorklist";
            public const string IsTaskAvailable = "IsTaskAvailable";
            public const string GetUserWorklist = "GetUserWorklist";
            public const string IsWorkItemAvailable = "IsWorkItemAvailable";

        }
        public class AuthenticationManager
        {
            public const string Controller = $"{_apiPrefi}/Authentication";
            public const string Login = "Login";
        }
    }
}
