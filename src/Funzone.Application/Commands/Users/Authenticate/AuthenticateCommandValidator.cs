using FluentValidation;

namespace Funzone.Application.Commands.Users.Authenticate
{
    public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateCommandValidator()
        {
            RuleFor(v => v.Email).NotNull().EmailAddress();
            RuleFor(v => v.Password).NotNull().Length(6, 50);
        }
    }
}