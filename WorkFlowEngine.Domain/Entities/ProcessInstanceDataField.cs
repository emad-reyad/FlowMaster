using System.ComponentModel.DataAnnotations;
using WorkFlowEngine.Domain.Primitives;

namespace WorkFlowEngine.Domain.Entities
{
    public class ProcessInstanceDataField : Entity
    {
        public int ProcessInstanceId { get; private set; }
        public virtual ProcessInstance ProcessInstance { get; private set; }
        public int DataFieldId { get; private set; }
        public DataField DataField { get; private set; }
        [MaxLength(4000)]
        public string? Value { get; private set; }
        private ProcessInstanceDataField()
        {

        }
        public ProcessInstanceDataField(DataField dataField, string? value)
        {
            this.DataFieldId = dataField.Id;
            this.DataField = dataField;
            this.Value = value;
        }
        public void SetDataValue(string value)
        {
            Value = value;
        }
    }
}
