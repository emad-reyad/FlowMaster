using FluentValidation;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetSerialNumber
{
    internal class GetSerialNumberValidator : AbstractValidator<GetSerialNumberRequest>
    {
        public GetSerialNumberValidator()
        {
            RuleFor(s => s.ApplicationNumber).NotNull().NotEmpty().WithMessage(Constants.Error.NotNull);
        }
    }
}
