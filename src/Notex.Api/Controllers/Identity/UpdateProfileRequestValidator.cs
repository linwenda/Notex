using FluentValidation;

namespace Notex.Api.Controllers.Identity;

public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
{
    public UpdateProfileRequestValidator()
    {
        RuleFor(v => v.Surname).MaximumLength(64);
        RuleFor(v => v.Bio).MaximumLength(128);
        RuleFor(v => v.Avatar).MaximumLength(512);
    }
}