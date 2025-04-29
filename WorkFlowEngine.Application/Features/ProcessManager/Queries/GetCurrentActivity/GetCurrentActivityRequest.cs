using MediatR;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetCurrentActivity
{
    public record GetCurrentActivityRequest(int ProcessInstanceId) : IRequest<Result<GetCurrentActivityResponse>>;
}
