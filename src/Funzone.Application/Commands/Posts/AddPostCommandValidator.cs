using FluentValidation;

namespace Funzone.Application.Commands.Posts
{
    public class AddPostCommandValidator : AbstractValidator<AddPostCommand>
    {
        public AddPostCommandValidator()
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