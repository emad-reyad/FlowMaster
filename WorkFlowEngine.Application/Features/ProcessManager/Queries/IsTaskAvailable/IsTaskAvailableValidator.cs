

using FluentValidation;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.IsTaskAvailable
{
    internal class IsTaskAvailableValidator : AbstractValidator<IsTaskAvailableRequest>
    {
        public IsTaskAvailableValidator()
        {
            RuleFor(s => s.ApplicationNumber).NotEmpty().NotNull().WithMessage(Constants.Error.NotNull);
        }
    }

}
