using MediatR;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetUserTaskInfo
{
    public record GetUserTaskInfoRequest(string ApplicationNumber, string UserId) : IRequest<Result<GetUserTaskInfoResponse>>;
}
