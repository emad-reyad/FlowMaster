using System.ComponentModel.DataAnnotations;
using WorkFlowEngine.Domain.Primitives;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Domain.Entities
{
    public class Process : Entity
    {
        public Process()
        {
            ProcessActivities = new HashSet<ProcessActivity>();
            ProcessDataFields = new HashSet<ProcessDataFields>();
            Transitions = new HashSet<Transition>();
        }
        [Required]
        [StringLength(1000)]
        public string Name { get; set; } = null!;
        [StringLength(4000)]
        public string Description { get; set; } = null!;
        public virtual ICollection<ProcessActivity> ProcessActivities { get; set; }
        public virtual ICollection<ProcessDataFields> ProcessDataFields { get; set; }
        public virtual ICollection<Transition> Transitions { get; set; }

        public Activity? getStartActivity()
            => ProcessActivities.Where(s => s.Activity.Name == Constants.Activities.StartActivity).Select(s => s.Activity).FirstOrDefault();
        public Activity? getEndActivity()
            => ProcessActivities.Where(s => s.Activity.Name == Constants.Activities.EndActivity).Select(s => s.Activity).FirstOrDefault();

        public Activity? GetActivity(int activityId)
            => ProcessActivities.Where(s => s.Activity.Id == activityId).Select(s => s.Activity).FirstOrDefault();

    }
}
