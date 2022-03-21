using FluentValidation;
using Notex.Messages.Users.Commands;

namespace Notex.Core.Validations;

public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
{
    public AuthenticateCommandValidator()
    {
        RuleFor(v => v.Email).EmailAddress();
        RuleFor(v => v.Password).NotNull().Length(6, 50);
    }
}