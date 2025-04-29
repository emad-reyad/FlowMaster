using MediatR;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Command.CancelProcessInstance
{
    public record CancelProcessInstanceRequest(int ProcessInstanceId, string UserId) : IRequest<Result<bool>>;
}
