using WorkFlowEngine.Application.Abstractions.ProcessManager;
using WorkFlowEngine.Domain.Entities;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Shared.Helper
{
    internal class InstanceAccess : IInstanceAccess
    {
        public Result<bool> HasInstanceAccess(ProcessInstance instance, string userId, List<string> userGroups)
        {
            var hasAccess = false;
            if (instance is null)
                return new Result<bool>(false, new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>(nameof(Constants.Error.NotFound),Constants.Error.NotFound)
                });
            if (instance.ProcessInstanceUsers is null || instance.ProcessInstanceUsers.Count == 0 || userGroups is null || userGroups.Count == 0)
                return new Result<bool>(false, new List<KeyValuePair<string, string>>
                {
                   new KeyValuePair<string, string>(nameof(Constants.Error.NoAssingnee), Constants.Error.NoAssingnee)
                });
            if (userGroups.Contains(Constants.Roles.PowerUser))
                hasAccess = true;
            else if (instance.Opened)
                hasAccess = instance.LastModifiedBy?.ToLower() == userId?.ToLower();
            else
            {
                foreach (var user in instance.ProcessInstanceUsers)
                {
                    if (user.IsActive && userGroups.Contains(user.DestinationName))
                    {
                        hasAccess = true;
                        break;
                    }
                }
            }
            return new Result<bool>(hasAccess, true);
        }
    }
}
