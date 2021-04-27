using Ardalis.GuardClauses;
using Funzone.Domain.SeedWork;
using Funzone.Domain.ZoneMembers;

namespace Funzone.Domain.Zones.Rules
{
    public class ZoneRuleCanBeDeletedOnlyByModeratorRule : IBusinessRule
    {
        private readonly ZoneMember _zoneMember;
        public ZoneRuleCanBeDeletedOnlyByModeratorRule(
            ZoneMember zoneMember)
        {
            _zoneMember = zoneMember;
        }

        public bool IsBroken()
        {
            return _zoneMember == null || !_zoneMember.IsModerator();
        }

        public string Message => "Only the moderator of a rule can deleted it.";
    }
}