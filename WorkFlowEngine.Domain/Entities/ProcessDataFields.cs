using WorkFlowEngine.Domain.Primitives;

namespace WorkFlowEngine.Domain.Entities
{
    public class ProcessDataFields : Entity
    {
        public int ProcessId { get; set; }
        public Process Process { get; set; }
        public int DataFieldId { get; set; }
        public DataField DataField { get; set; }
        public bool IsRequired { get; set; }
        public string? DefaultValue { get; set; }
    }
}
