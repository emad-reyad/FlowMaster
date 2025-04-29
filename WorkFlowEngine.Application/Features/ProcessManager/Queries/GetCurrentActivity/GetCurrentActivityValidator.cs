using FluentValidation;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetCurrentActivity
{
    public class GetCurrentActivityValidator : AbstractValidator<GetCurrentActivityRequest>
    {
        public GetCurrentActivityValidator()
        {
            RuleFor(s => s.ProcessInstanceId).NotEmpty().GreaterThan(0).WithMessage(Constants.Error.NotNull);
        }
    }
}
