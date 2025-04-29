using WorkFlowEngine.Domain.Primitives;

namespace WorkFlowEngine.Domain.Entities
{
    public class Action : BaseLookup
    {
        public Action()
        {
            ActivityActions = new HashSet<ActivityAction>();
        }
        public virtual ICollection<ActivityAction> ActivityActions { get; set; }
    }
}
