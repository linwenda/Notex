using Funzone.Domain.SeedWork;
using Funzone.Domain.ZoneUsers;

namespace Funzone.Domain.Posts.Rules
{
    public class PostCanBeReviewedOnlyByModeratorRule:IBusinessRule
    {
        private readonly ZoneUser _zoneUser;

        public PostCanBeReviewedOnlyByModeratorRule(ZoneUser zoneUser)
        {
            _zoneUser = zoneUser;
        }

        public bool IsBroken() => !_zoneUser.IsModerator();

        public string Message => "Only moderator can be review";
    }
}
