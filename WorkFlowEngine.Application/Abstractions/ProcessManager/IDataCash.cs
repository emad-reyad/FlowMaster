using WorkFlowEngine.Domain.Entities;


namespace WorkFlowEngine.Application.Abstractions.ProcessManager
{
    public interface IDataCash
    {
        public List<string> GetUserGroups(string userId);
        public Task<Process> GetProcessByName(string processName);
        public Task<Process> GetProcessById(int processId);
    }
}
