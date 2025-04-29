using WorkFlowEngine.Domain.Primitives;

namespace WorkFlowEngine.Domain.Entities
{
    public class ProcessInstanceHistory : Entity
    {
        public int ProcessInstanceId { get; set; }
        public virtual ProcessInstance ProcessInstance { get; private set; }
        public int CurrentActivityId { get; set; }
        public virtual Activity CurrentActivity { get; private set; } = null!;
        public int ActionId { get; set; }
        public virtual Action Action { get; private set; } = null!;
        public int NextActivityId { get; set; }
        public string UserId { get; set; }
        public virtual Activity NextActivity { get; private set; } = null!;
        public DateTime ActionDate { get; private set; }


        public ProcessInstanceHistory(int currentActivityId, int actionId, int nextActivityId, string userId)
        {
            CurrentActivityId = currentActivityId;
            ActionId = actionId;
            NextActivityId = nextActivityId;
            ActionDate = DateTime.Now;
            UserId = userId;
        }


    }
}
