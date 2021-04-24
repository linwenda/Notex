using Funzone.Domain.SeedWork;
using Funzone.Domain.ZoneUsers;

namespace Funzone.Domain.ZoneRules.Rules
{
    public class ZoneRuleCannotDeletedByMemberRule : IBusinessRule
    {
        private readonly ZoneUserRole _role;
        public ZoneRuleCannotDeletedByMemberRule(
            ZoneUserRole role)
        {
            _role = role;
        }

        public bool IsBroken() => _role == ZoneUserRole.Member;

        public string Message => "Only the moderator of a rule can delete it.";
    }
}