using WorkFlowEngine.Application.Features.ProcessManager.Shared.Models;

namespace WorkFlowEngine.Application.Features.ProcessManager.Command.ExecuteActivityAction
{
    public record ExecuteActivityActionResponse(int Id, CurrentActivity CurrentActivity);
}
