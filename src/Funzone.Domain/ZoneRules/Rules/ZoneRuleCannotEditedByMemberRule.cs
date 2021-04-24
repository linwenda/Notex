using Funzone.Domain.SeedWork;
using Funzone.Domain.ZoneUsers;

namespace Funzone.Domain.ZoneRules.Rules
{
    public class ZoneRuleCannotEditedByMemberRule : IBusinessRule
    {
        private readonly ZoneUserRole _role;
        public ZoneRuleCannotEditedByMemberRule(
            ZoneUserRole role)
        {
            _role = role;
        }

        public bool IsBroken() => _role == ZoneUserRole.Member;

        public string Message => "Only the moderator of a rule can edit it.";
    }
}