using FluentValidation;

namespace Notex.Api.Controllers.Identity;

public class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequest>
{
    public AuthenticateRequestValidator()
    {
        RuleFor(v => v.Email).EmailAddress();
        RuleFor(v => v.Password).NotNull().Length(6, 50);
    }
}