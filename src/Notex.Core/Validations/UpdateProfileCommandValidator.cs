using FluentValidation;
using Notex.Messages.Users.Commands;

namespace Notex.Core.Validations;

public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
{
    public UpdateProfileCommandValidator()
    {
        RuleFor(v => v.Name).MaximumLength(64);
        RuleFor(v => v.Bio).MaximumLength(128);
        RuleFor(v => v.Avatar).MaximumLength(512);
    }
}