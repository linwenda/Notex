using FluentValidation;
using Notex.Messages.Notes.Commands;

namespace Notex.Core.Validations;

public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
{
    public CreateNoteCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotNull()
            .NotEmpty()
            .MaximumLength(64);
    }
}