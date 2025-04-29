using WorkFlowEngine.Domain.Primitives;

namespace WorkFlowEngine.Domain.Entities
{
    public class ProcessInstanceUser : Entity
    {
        public int ProcessInstanceId { get; private set; }
        public virtual ProcessInstance ProcessInstance { get; set; }
        public int DestinationTypeId { get; private set; }
        public virtual DestinationType DestinationType { get; private set; }
        public string DestinationName { get; private set; }
        public bool IsActive { get; private set; }

        public ProcessInstanceUser(int destinationTypeId, string destinationName)
        {
            DestinationTypeId = destinationTypeId;
            DestinationName = destinationName;
            IsActive = true;
        }
        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
