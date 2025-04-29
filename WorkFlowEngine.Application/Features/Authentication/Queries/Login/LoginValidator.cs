using FluentValidation;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.Authentication.Queries.Login
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(s => s.Username).NotEmpty().NotEmpty().WithMessage(Constants.Error.NotNull);
            RuleFor(s => s.Password).NotEmpty().NotEmpty().WithMessage(Constants.Error.NotNull);
        }
    }
}
