using WorkFlowEngine.Domain.Entities;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Abstractions.ProcessManager
{
    public interface IInstanceAccess
    {
        public Result<bool> HasInstanceAccess(ProcessInstance processInstance, string userId, List<string> userGroups);
    }
}
