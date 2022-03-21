using FluentValidation;
using Notex.Messages.Comments.Commands;

namespace Notex.Core.Validations;

public class AddMergeRequestCommentCommandValidator : AbstractValidator<AddMergeRequestCommentCommand>
{
    public AddMergeRequestCommentCommandValidator()
    {
        RuleFor(v => v.Text)
            .NotNull()
            .NotEmpty()
            .MaximumLength(1024);
    }
}