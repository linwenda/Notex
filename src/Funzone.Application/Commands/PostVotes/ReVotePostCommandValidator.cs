using FluentValidation;

namespace Funzone.Application.Commands.PostVotes
{
    public class ReVotePostCommandValidator : AbstractValidator<ReVotePostCommand>
    {
        public ReVotePostCommandValidator()
        {
            RuleFor(v => v.VoteType)
                .Must(VoteTypeValidator.IsSupportType);
        }
    }
}