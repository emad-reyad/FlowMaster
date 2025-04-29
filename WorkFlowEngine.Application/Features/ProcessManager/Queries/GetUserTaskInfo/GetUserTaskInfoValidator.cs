using FluentValidation;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetUserTaskInfo
{
    public class GetUserTaskInfoValidator : AbstractValidator<GetUserTaskInfoRequest>
    {
        public GetUserTaskInfoValidator()
        {
            RuleFor(s => s.ApplicationNumber).NotEmpty().NotNull().WithMessage(Constants.Error.NotNull);
        }
    }
}
