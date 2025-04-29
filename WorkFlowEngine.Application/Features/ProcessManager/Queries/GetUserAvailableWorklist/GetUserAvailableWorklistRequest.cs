using MediatR;
using WorkFlowEngine.Application.Features.ProcessManager.Shared.Models;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetUserAvailableWorklist
{
    public record GetUserAvailableWorklistRequest(
        string UserId,
        DateTime? DateFrom,
        DateTime? DateTo,
        string ApplicantNumber = default!,
        string ApplicationNumber = default!,
        List<string> ProcessNames = default!) : IRequest<Result<List<Worklist>>>;

}
