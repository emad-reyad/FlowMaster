using FluentValidation;

namespace WorkFlowEngine.Application.Features.ProcessManagerFeatures.Command.StartProcessInstance
{
    internal class StartProcessInstanceValidator : AbstractValidator<StartProcessInstanceRequest>
    {
        public StartProcessInstanceValidator()
        {
            RuleFor(s => s.ProcessName).NotNull().NotEmpty().WithErrorCode("400").
                WithMessage(s => $"{s.ProcessName} is required");
            RuleFor(s => s.DataFields).NotNull().WithErrorCode("400").
                WithMessage(s => $"{s.DataFields} is required");
            RuleFor(s => s.UserId).NotNull().WithErrorCode("400").
                WithMessage(s => $"{s.UserId} is required");
        }
    }
}
