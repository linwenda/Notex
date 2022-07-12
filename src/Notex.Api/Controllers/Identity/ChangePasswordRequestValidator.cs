using FluentValidation;

namespace Notex.Api.Controllers.Identity;

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(v => v.CurrentPassword).NotNull().Length(6, 50);
        RuleFor(v => v.NewPassword).NotNull().Length(6, 50);
    }
}