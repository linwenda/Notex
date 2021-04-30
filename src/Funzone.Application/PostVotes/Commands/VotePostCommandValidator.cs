using FluentValidation;

namespace Funzone.Application.PostVotes.Commands
{
    public class VotePostCommandValidator : AbstractValidator<VotePostCommand>
    {
        public VotePostCommandValidator()
        {
            RuleFor(v => v.VoteType)
                .Must(PostVoteValidator.IsSupportVoteType);
        }
    }
}