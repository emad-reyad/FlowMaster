using WorkFlowEngine.Application.Features.ProcessManager.Shared.Models;

namespace WorkFlowEngine.Application.Features.ProcessManagerFeatures.Command.StartProcessInstance
{
    //public sealed record StartProcessInstanceResponse(int Id,int CurrentActivityId, CurrentActivity CurrentActivity);

    public record StartProcessInstanceResponse(int Id, CurrentActivity CurrentActivity);
}

