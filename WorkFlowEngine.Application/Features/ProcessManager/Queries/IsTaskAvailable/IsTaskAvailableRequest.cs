using MediatR;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.IsTaskAvailable
{
    public record IsTaskAvailableRequest(string ApplicationNumber, string UserId) : IRequest<Result<int>>;

}
