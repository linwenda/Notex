using Funzone.Domain.SeedWork;
using Funzone.Domain.ZoneMembers;

namespace Funzone.Domain.PostDrafts.Rules
{
    public class PostDraftCanBeAddedOnlyByZoneMemberRule : IBusinessRule
    {
        private readonly ZoneMember _member;

        public PostDraftCanBeAddedOnlyByZoneMemberRule(ZoneMember member)
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