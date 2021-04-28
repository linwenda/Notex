using FluentValidation;

namespace Funzone.Application.Commands.Posts
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