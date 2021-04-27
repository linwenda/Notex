using Funzone.Domain.SeedWork;
using Funzone.Domain.ZoneMembers;

namespace Funzone.Domain.Zones.Rules
{
    public class ZoneRuleCanBeAddedOnlyByModeratorRule : IBusinessRule
    {
        private readonly ZoneMember _zoneMember;
        public ZoneRuleCanBeAddedOnlyByModeratorRule(
            ZoneMember zoneMember)
        {
            _zoneMember = zoneMember;
        }

        public bool IsBroken()
        {
            return _zoneMember == null || !_zoneMember.IsModerator();
        }

        public string Message => "Only the moderator of a rule can add it.";
    }
}