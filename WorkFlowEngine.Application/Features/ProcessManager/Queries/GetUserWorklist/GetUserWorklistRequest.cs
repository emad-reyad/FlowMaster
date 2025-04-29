using MediatR;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetUserWorklistStatistics
{
    public record GetUserWorklistRequest(string UserId, DateTime? DateFrom, DateTime? DateTo) : IRequest<Result<List<GetUserWorklistResponse>>>;
}
