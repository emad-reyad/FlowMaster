using FluentValidation;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Command.CancelProcessInstance
{
    public class CancelProcessInstanceValidator : AbstractValidator<CancelProcessInstanceRequest>
    {
        public CancelProcessInstanceValidator()
        {
            RuleFor(s => s.ProcessInstanceId).GreaterThan(0).WithMessage(Constants.Error.NotNull);
            RuleFor(s => s.UserId).NotNull().NotEmpty().WithMessage(Constants.Error.NotNull);
        }
    }
}
