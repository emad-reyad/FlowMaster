using WorkFlowEngine.Application.Features.ProcessManager.Queries.GetCurrentActivity;
using WorkFlowEngine.Application.Features.ProcessManagerFeatures.Command.OpenTask;
using WorkFlowEngine.Application.Features.ProcessManagerFeatures.Command.StartProcessInstance;

namespace WorkFlowEngine.Application.Abstractions.ProcessManager
{
    public interface IProcessManagerCommandService
    {
        Task<StartProcessInstanceResponse> StartProcessInstance(StartProcessInstanceRequest processInstance);
        Task<bool> OpenTask(OpenTaskRequest request);
        Task<int> ExecuteActivityAction(GetCurrentActivityRequest request);
        Task<GetCurrentActivityResponse> GetCurrentActivity(int processInstnaceId);
        Task<bool> ReleaseTask(int processInstnaceId);
        Task<bool> CancelProcessInstance(int processInstnaceId);
    }
}
