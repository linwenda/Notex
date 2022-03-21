using FluentValidation;
using Notex.Messages.Notes.Commands;

namespace Notex.Core.Validations;

public class EditNoteCommandValidator : AbstractValidator<EditNoteCommand>
{
    public EditNoteCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotNull()
            .NotEmpty()
            .MaximumLength(64);
    }
}