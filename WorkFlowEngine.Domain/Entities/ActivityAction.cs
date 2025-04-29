using WorkFlowEngine.Domain.Primitives;

namespace WorkFlowEngine.Domain.Entities
{
    public class ActivityAction : Entity
    {
        public int ActionId { get; set; }
        public virtual Action Action { get; set; }
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
    }
}
