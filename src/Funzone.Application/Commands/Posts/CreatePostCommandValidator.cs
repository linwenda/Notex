using System.Linq;
using FluentValidation;
using Funzone.Domain.Posts;

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
                .NotNull()
                .NotEmpty()
                .Must(t => PostType.SupportTypes.Any(type => type.Value == t));
        }
    }
}