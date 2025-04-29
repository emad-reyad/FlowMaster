using FluentValidation;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetActivityActions
{
    public class GetActivityActionsValidator : AbstractValidator<GetActivityActionsRequest>
    {
        public GetActivityActionsValidator()
        {
            RuleFor(s => s.ProcessInstanceId).GreaterThan(0).WithMessage(Constants.Error.NotNull);
            RuleFor(s => s.UserId).NotNull().NotEmpty().WithMessage(Constants.Error.NotNull);
        }
    }
}
