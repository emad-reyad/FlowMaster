using FluentValidation;

namespace WorkFlowEngine.Application.Features.ProcessManager.Command.ExecuteActivityAction
{
    public class ExecuteActivityValidator : AbstractValidator<ExecuteActivityActionRequest>
    {
        public ExecuteActivityValidator()
        {
            RuleFor(s => s.ProcessInstanceId).GreaterThan(0).WithMessage("Invalid process Id");
            RuleFor(s => s.Action).NotNull().NotEmpty().WithMessage("Action can not be empty or null");
            RuleFor(s => s.UserId).NotNull().NotEmpty().WithMessage("Action can not be empty or null");
        }
    }
}
