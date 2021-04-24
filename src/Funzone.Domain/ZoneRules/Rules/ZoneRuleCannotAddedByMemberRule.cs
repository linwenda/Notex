using Funzone.Domain.SeedWork;
using Funzone.Domain.ZoneUsers;

namespace Funzone.Domain.ZoneRules.Rules
{
    public class ZoneRuleCannotAddedByMemberRule : IBusinessRule
    {
        private readonly ZoneUserRole _role;
        public ZoneRuleCannotAddedByMemberRule(
            ZoneUserRole role)
        {
            _role = role;
        }

        public bool IsBroken() => _role == ZoneUserRole.Member;

        public string Message => "Only the moderator of a rule can add it.";
    }
}