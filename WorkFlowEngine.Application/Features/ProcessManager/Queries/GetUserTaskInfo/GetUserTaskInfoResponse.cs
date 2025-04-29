using WorkFlowEngine.Application.Features.ProcessManager.Shared.Models;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetUserTaskInfo
{
    public record GetUserTaskInfoResponse(int Id, string TaskUrl, CurrentActivity CurrentActivity);
}
