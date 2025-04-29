using MediatR;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManagerFeatures.Command.OpenTask
{
    public record OpenTaskRequest(int ProcessInstanceId, string UserId) : IRequest<Result<bool>>;

}
