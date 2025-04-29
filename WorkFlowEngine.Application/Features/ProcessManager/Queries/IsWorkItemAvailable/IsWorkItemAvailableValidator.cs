

using FluentValidation;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.IsTaskAvailable
{
    internal class IsWorkItemAvailableValidator : AbstractValidator<IsTaskAvailableRequest>
    {
        public IsWorkItemAvailableValidator()
        {
            RuleFor(s => s.ApplicationNumber).NotEmpty().NotNull().WithMessage(Constants.Error.NotNull);
        }
    }

}
