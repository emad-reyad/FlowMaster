using WorkFlowEngine.Domain.Primitives;

namespace WorkFlowEngine.Domain.Entities
{
    public class DestinationType : BaseLookup
    {
        public DestinationType()
        {
            ActivityDestinations = new HashSet<ActivityDestinationType>();
        }
        public virtual ICollection<ActivityDestinationType> ActivityDestinations { get; set; }
    }
}
