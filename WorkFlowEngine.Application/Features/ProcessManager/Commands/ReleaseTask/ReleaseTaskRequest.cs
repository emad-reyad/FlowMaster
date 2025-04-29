using MediatR;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManagerFeatures.Command.ReleaseTask
{
    public record ReleaseTaskRequest(int ProcessInstanceId, string UserId) : IRequest<Result<bool>>;
}
