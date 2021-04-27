using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.PostCommentVotes
{
    public class PostCommentCanBeVotedOnlyByVoterRule : IBusinessRule
    {
        private readonly UserId _voterId;
        private readonly UserId _userId;

        public PostCommentCanBeVotedOnlyByVoterRule(UserId voterId, UserId userId)
        {
            _voterId = voterId;
            _userId = userId;
        }

        public bool IsBroken() => _voterId != _userId;

        public string Message => "Only voter can vote posts.";
    }
}