using FluentValidation;

namespace Funzone.IdentityAccess.Application.Commands.Authenticate
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