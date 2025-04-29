using MediatR;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.IsWorkItemAvailable
{
    public record IsWorkItemAvailableRequest(int ProcessInstanceId, string UserId) : IRequest<Result<bool>>;

}
