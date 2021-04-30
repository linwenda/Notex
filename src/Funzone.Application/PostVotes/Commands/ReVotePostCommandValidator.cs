using FluentValidation;

namespace Funzone.Application.PostVotes.Commands
{
    public class ReVotePostCommandValidator : AbstractValidator<ReVotePostCommand>
    {
        public ReVotePostCommandValidator()
        {
            RuleFor(v => v.VoteType)
                .Must(PostVoteValidator.IsSupportVoteType);
        }
    }
}