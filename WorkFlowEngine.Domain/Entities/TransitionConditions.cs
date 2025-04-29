using WorkFlowEngine.Domain.Enums;
using WorkFlowEngine.Domain.Primitives;

namespace WorkFlowEngine.Domain.Entities
{
    public class TransitionConditions : Entity
    {
        public int TransitionId { get; set; }
        public virtual Transition Transition { get; set; }
        public BinaryOperator BinaryOperator { get; set; }
        public int DataFieldId { get; set; }
        public DataField DataField { get; set; }
        public Operator Operator { get; set; }
        public string Value { get; set; }
        public bool MatchsCondition(int dataFieldId, string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;
            switch (@Operator)
            {
                case Operator.Equal:
                    return Value == value;
                case Operator.NotEqual:
                    return Value != value;
                case Operator.LessThan:
                    return int.Parse(value) < int.Parse(Value);
                case Operator.GreaterThan:
                    return int.Parse(value) > int.Parse(Value);
                case Operator.LessThanOrEqual:
                    return int.Parse(value) <= int.Parse(Value);
                case Operator.GreaterThanOrEqual:
                    return int.Parse(value) >= int.Parse(Value);
                default:
                    return false;
            }
        }
    }




}
