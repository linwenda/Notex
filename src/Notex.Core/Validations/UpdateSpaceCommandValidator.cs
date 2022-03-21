using FluentValidation;
using Notex.Messages.Spaces.Commands;

namespace Notex.Core.Validations;

public class UpdateSpaceCommandValidator : AbstractValidator<UpdateSpaceCommand>
{
    public UpdateSpaceCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(32);
    }
}