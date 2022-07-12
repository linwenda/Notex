using FluentValidation;

namespace Notex.Api.Controllers.Identity;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(r => r.Email).EmailAddress();
        
        RuleFor(v => v.Surname)
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