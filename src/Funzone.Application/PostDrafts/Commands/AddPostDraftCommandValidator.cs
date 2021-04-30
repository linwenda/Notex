using FluentValidation;
using Funzone.Application.Posts;
using Funzone.Application.Posts.Commands;

namespace Funzone.Application.PostDrafts.Commands
{
    public class AddPostDraftCommandValidator : AbstractValidator<AddPostDraftCommand>
    {
        public AddPostDraftCommandValidator()
        {
            RuleFor(v => v.Title)
                .NotNull().NotEmpty().MaximumLength(50);

            RuleFor(v => v.Content)
                .MaximumLength(2048);

            RuleFor(v => v.Type)
                .Must(PostValidator.IsSupportType);
        }
    }
}