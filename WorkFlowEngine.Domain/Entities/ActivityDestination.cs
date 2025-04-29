using WorkFlowEngine.Domain.Primitives;

namespace WorkFlowEngine.Domain.Entities
{
    public class ActivityDestinationType : Entity
    {
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
        public int DestinationTypeID { get; set; }
        public virtual DestinationType DestinationType { get; set; }
        public virtual DataField ValueDataField { get; set; }
        public int ValueDataFieldId { get; set; }
    }
}
