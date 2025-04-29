using MediatR;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetActivityActions
{
    public record GetActivityActionsRequest(int ProcessInstanceId, string UserId) : IRequest<Result<List<string>>>;

}
