using FluentValidation;
using Notex.Messages.Comments.Commands;

namespace Notex.Core.Validations;

public class AddNoteCommentCommandValidator : AbstractValidator<AddCommentReplyCommand>
{
    public AddNoteCommentCommandValidator()
    {
        RuleFor(v => v.Text)
            .NotNull()
            .NotEmpty()
            .MaximumLength(1024);
    }
}