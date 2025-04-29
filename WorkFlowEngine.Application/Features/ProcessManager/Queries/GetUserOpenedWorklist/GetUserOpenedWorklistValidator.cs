using FluentValidation;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetUserOpenedWorklist
{
    public class GetUserOpenedWorklistValidator : AbstractValidator<GetUserOpenedWorklistRequest>
    {
        public GetUserOpenedWorklistValidator()
        {
            RuleFor(s => s.UserId).NotNull().NotEmpty().WithMessage(Constants.Error.NotNull);
        }
    }
}
