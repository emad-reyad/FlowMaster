using WorkFlowEngine.Application.Features.ProcessManager.Shared.Models;

namespace WorkFlowEngine.Application.Abstractions.ProcessManager
{
    public interface IProcessManagerQueryService
    {
        Task<List<Worklist>> GetActivityActions(int processInstnaceId);
        Task<bool> IsAvailableTask(int processInstnaceId);
        Task<List<string>> GetActivityActionNames(int processInstnaceId);
        Task<string> GetCurrentActivityMode(int processInstnaceId);
        Task<string> GetDataFieldValue(int processInstnaceId, string DataFieldName);
        Task<string> GetUserAvailableWorklist(string username, DateTime dateFrom, DateTime DateTo, int processInstnaceId = 0);
    }
}
