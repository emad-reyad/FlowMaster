using MediatR;
using WorkFlowEngine.Application.Features.ProcessManager.Shared.Models;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetUserOpenedWorklist
{
    public record GetUserOpenedWorklistRequest(string UserId) : IRequest<Result<List<Worklist>>>;

}
