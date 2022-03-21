using FluentValidation;
using Notex.Messages.Comments.Commands;

namespace Notex.Core.Validations;

public class EditCommentCommandValidator : AbstractValidator<EditCommentCommand>
{
    public EditCommentCommandValidator()
    {
        RuleFor(v => v.Text)
            .NotNull()
            .NotEmpty()
            .MaximumLength(1024);
    }
}