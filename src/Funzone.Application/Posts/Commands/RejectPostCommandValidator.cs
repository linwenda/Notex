using FluentValidation;

namespace Funzone.Application.Posts.Commands
{
    public class RejectPostCommandValidator : AbstractValidator<RejectPostCommand>
    {
        public RejectPostCommandValidator()
        {
            RuleFor(v => v.Reason)
                .NotNull()
                .NotEmpty()
                .MaximumLength(256);
        }
    }
}