using WorkFlowEngine.Domain.Primitives;

namespace WorkFlowEngine.Domain.Entities
{
    public class ProcessActivity : Entity
    {
        public int ProcessId { get; set; }
        public virtual Process Process { get; set; }
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; } = null!;

    }
}
