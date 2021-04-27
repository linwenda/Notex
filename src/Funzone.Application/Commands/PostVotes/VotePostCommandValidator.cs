using FluentValidation;

namespace Funzone.Application.Commands.PostVotes
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