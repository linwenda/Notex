using FluentValidation;
using Notex.Messages.Users.Commands;

namespace Notex.Core.Validations;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(r => r.Email).EmailAddress();
        
        RuleFor(v => v.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(r => r.Password)
            .NotNull()
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(64);
    }
}