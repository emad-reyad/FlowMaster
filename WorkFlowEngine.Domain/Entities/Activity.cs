using WorkFlowEngine.Domain.Enums;
using WorkFlowEngine.Domain.Primitives;

namespace WorkFlowEngine.Domain.Entities
{
    public class Activity : BaseLookup
    {
        //public Activity(int id) : base(id)
        //{
        //} 
        public Activity()
        {
            ActivityDestinationTypes = new HashSet<ActivityDestinationType>();
            ActivityActions = new HashSet<ActivityAction>();
            ProcessActivities = new HashSet<ProcessActivity>();
        }
        public RuleOption RuleOption { get; set; }
        public ActivityMode Mode { get; set; }
        public ActivityType ActivityType { get; set; }
        public virtual ICollection<ProcessActivity> ProcessActivities { get; set; }
        public virtual ICollection<ActivityDestinationType> ActivityDestinationTypes { get; set; }
        public virtual ICollection<ActivityAction> ActivityActions { get; set; }

    }
}
