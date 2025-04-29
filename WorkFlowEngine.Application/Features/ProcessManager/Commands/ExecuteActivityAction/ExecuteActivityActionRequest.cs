using MediatR;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Command.ExecuteActivityAction
{
    public record ExecuteActivityActionRequest(int ProcessInstanceId, string UserId, string Action, Dictionary<string, string> DataFields = null!) : IRequest<Result<ExecuteActivityActionResponse>>;
}
