namespace WorkFlowEngine.Application.Features.ProcessManager.Shared.Models
{
    public record Worklist(int Id, string ApplicationNumber, string TaskUrl, DateTime LastModificationDate, CurrentActivity CurrentActivity, bool Opened);
}
