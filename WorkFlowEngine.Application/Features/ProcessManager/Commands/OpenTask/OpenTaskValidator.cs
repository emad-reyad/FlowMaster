using FluentValidation;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManagerFeatures.Command.OpenTask
{
    internal class OpenTaskValidator : AbstractValidator<OpenTaskRequest>
    {
        public OpenTaskValidator()
        {
            RuleFor(s => s.ProcessInstanceId).GreaterThan(0).WithMessage(Constants.Error.IsMissing);
            RuleFor(s => s.UserId).NotEmpty().NotNull().WithMessage(Constants.Error.NotNull);
        }
    }
}
