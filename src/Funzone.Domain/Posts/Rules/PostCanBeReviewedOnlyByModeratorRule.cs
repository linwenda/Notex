using Funzone.Domain.SeedWork;
using Funzone.Domain.ZoneMembers;

namespace Funzone.Domain.Posts.Rules
{
    public class PostCanBeReviewedOnlyByModeratorRule : IBusinessRule
    {
        private readonly ZoneMember _member;

        public PostCanBeReviewedOnlyByModeratorRule(ZoneMember member)
        {
            _member = member;
        }

        public bool IsBroken() => _member == null || !_member.IsModerator();

        public string Message => "Only moderator can be review.";
    }
}