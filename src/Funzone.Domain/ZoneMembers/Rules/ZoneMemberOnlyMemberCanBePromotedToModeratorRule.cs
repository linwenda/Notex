using Funzone.Domain.SeedWork;

namespace Funzone.Domain.ZoneMembers.Rules
{
    public class ZoneMemberOnlyMemberCanBePromotedToModeratorRule : IBusinessRule
    {
        private readonly ZoneMemberRole _role;

        public ZoneMemberOnlyMemberCanBePromotedToModeratorRule(ZoneMemberRole role)
        {
            _role = role;
        }

        public bool IsBroken()
        {
            return _role != ZoneMemberRole.Member;
        }

        public string Message => "Only member can be promoted to moderator.";
    }
}