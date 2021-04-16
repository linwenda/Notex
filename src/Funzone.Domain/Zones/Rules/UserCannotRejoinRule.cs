using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.Zones.Rules
{
    public class UserCannotRejoinRule : IBusinessRule
    {
        private readonly IZoneCounter _zoneCounter;
        private readonly UserId _userId;

        public UserCannotRejoinRule(IZoneCounter zoneCounter, UserId userId)
        {
            _zoneCounter = zoneCounter;
            _userId = userId;
        }

        public bool IsBroken()
        {
            return _zoneCounter.CountZoneMemberWithUserId(_userId) > 0;
        }

        public string Message => "You have joined successfully.";
    }
}