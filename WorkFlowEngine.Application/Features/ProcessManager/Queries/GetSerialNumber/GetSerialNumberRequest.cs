using MediatR;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetSerialNumber
{
    public record GetSerialNumberRequest(string ApplicationNumber) : IRequest<Result<int>>;

}
