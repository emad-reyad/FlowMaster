using FluentValidation;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManagerFeatures.Command.ReleaseTask
{
    public class ReleaseTaskValidator : AbstractValidator<ReleaseTaskRequest>
    {
        public ReleaseTaskValidator()
        {
            RuleFor(s => s.ProcessInstanceId).GreaterThan(0).WithMessage(Constants.Error.IsMissing);
            RuleFor(s => s.UserId).NotNull().NotEmpty().WithMessage(Constants.Error.IsMissing);
        }
    }
}
