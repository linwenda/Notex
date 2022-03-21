using FluentValidation;
using Notex.Messages.Comments.Commands;

namespace Notex.Core.Validations;

public class AddCommentReplyCommandValidator : AbstractValidator<AddCommentReplyCommand>
{
    public AddCommentReplyCommandValidator()
    {
        RuleFor(v => v.Text)
            .NotNull()
            .NotEmpty()
            .MaximumLength(1024);
    }
}