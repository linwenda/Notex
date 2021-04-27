using Funzone.Domain.SeedWork;
using Funzone.Domain.ZoneMembers;

namespace Funzone.Domain.Zones.Rules
{
    public class ZonePostCanBeCreatedOnlyByMemberRule : IBusinessRule
    {
        private readonly ZoneMember _zoneMember;

        public ZonePostCanBeCreatedOnlyByMemberRule(ZoneMember zoneMember)
        {
            _zoneMember = zoneMember;
        }

        public bool IsBroken()
        {
            return _zoneMember == null || _zoneMember.IsLeave;
        }

        public string Message => "Only zone member can create posts.";
    }
}