using Ardalis.GuardClauses;
using Funzone.Domain.SeedWork;
using Funzone.Domain.ZoneUsers;

namespace Funzone.Domain.Zones.Rules
{
    public class ZoneRuleCanBeDeletedOnlyByModeratorRule : IBusinessRule
    {
        private readonly ZoneUser _zoneMember;
        public ZoneRuleCanBeDeletedOnlyByModeratorRule(
            ZoneUser zoneMember)
        {
            Guard.Against.Null(zoneMember, nameof(ZoneUser));
            _zoneMember = zoneMember;
        }

        public bool IsBroken() => !_zoneMember.IsModerator();

        public string Message => "Only the moderator of a rule can deleted it.";
    }
}