using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.PostVotes
{
    public class PostCanBeReVotedOnlyByVoterRule : IBusinessRule
    {
        private readonly UserId _voterId;
        private readonly UserId _userId;

        public PostCanBeReVotedOnlyByVoterRule(UserId voterId, UserId userId)
        {
            _voterId = voterId;
            _userId = userId;
        }

        public bool IsBroken() => _voterId != _userId;

        public string Message => "Only voter can vote posts.";
    }
}