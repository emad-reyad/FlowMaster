using WorkFlowEngine.Domain.Primitives;

namespace WorkFlowEngine.Domain.Entities
{
    public class Transition : Entity
    {
        public Transition()
        {
            TransitionConditions = new HashSet<TransitionConditions>();
        }
        public int ProcessId { get; set; }
        public virtual Process Process { get; set; }
        public int CurrentActivityId { get; set; }
        public virtual Activity CurrentActivity { get; set; } = null!;
        public int ActionId { get; set; }
        public virtual Action Action { get; set; } = null!;
        public int NextActivityId { get; set; }
        public virtual Activity NextActivity { get; set; } = null!;
        public virtual ICollection<TransitionConditions> TransitionConditions { get; set; }
    }
}
