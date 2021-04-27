using FluentValidation;

namespace Funzone.Application.Commands.Posts
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(v => v.Title)
                .NotNull().NotEmpty().MaximumLength(50);

            RuleFor(v => v.Content)
                .MaximumLength(2056);

            RuleFor(v => v.Type)
                .Must(PostValidator.IsSupportType);
        }
    }
}