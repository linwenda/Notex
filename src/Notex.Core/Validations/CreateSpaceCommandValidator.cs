using FluentValidation;
using Notex.Messages.Spaces.Commands;

namespace Notex.Core.Validations;

public class CreateSpaceCommandValidator : AbstractValidator<CreateSpaceCommand>
{
    public CreateSpaceCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50);
    }
}