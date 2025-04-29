using MediatR;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManagerFeatures.Command.StartProcessInstance
{
    public record StartProcessInstanceRequest(string ProcessName, string UserId, string ApplicationNumber, string ApplicantNumber, Dictionary<string, string> DataFields) : IRequest<Result<StartProcessInstanceResponse>>;
}
