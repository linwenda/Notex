using Funzone.Domain.SeedWork;
using Funzone.Domain.ZoneMembers;

namespace Funzone.Domain.ZoneRules.Rules
{
    public class ZoneRuleCannotAddedByMemberRule : IBusinessRule
    {
        private readonly ZoneMemberRole _role;

        public ZoneRuleCannotAddedByMemberRule(
            ZoneMemberRole role)
        {
            _role = role;
        }

        public bool IsBroken() => _role == ZoneMemberRole.Member;

        public string Message => "Only the moderator of a rule can add it.";
    }
}