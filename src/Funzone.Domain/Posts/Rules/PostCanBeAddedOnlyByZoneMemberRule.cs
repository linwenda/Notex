using Funzone.Domain.SeedWork;
using Funzone.Domain.ZoneMembers;

namespace Funzone.Domain.Posts.Rules
{
    public class PostCanBeAddedOnlyByZoneMemberRule : IBusinessRule
    {
        private readonly ZoneMember _member;

        public PostCanBeAddedOnlyByZoneMemberRule(ZoneMember member)
        {
            _member = member;
        }

        public bool IsBroken()
        {
            return _member == null || _member.IsLeave;
        }

        public string Message => "Only zone member can create posts.";
    }
}