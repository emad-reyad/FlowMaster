using WorkFlowEngine.Domain.Primitives;

namespace WorkFlowEngine.Domain.Entities
{
    public class DataField : BaseLookup
    {
        public DataField()
        {
            ProcessDataFields = new HashSet<ProcessDataFields>();
        }
        public ICollection<ProcessDataFields> ProcessDataFields { get; set; }
    }
}
