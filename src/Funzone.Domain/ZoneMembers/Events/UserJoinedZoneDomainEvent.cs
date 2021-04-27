using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;

namespace Funzone.Domain.ZoneMembers.Events
{
    public class UserJoinedZoneDomainEvent : DomainEventBase
    {
        public ZoneId ZoneId { get; }
        public UserId UserId { get; }

        public UserJoinedZoneDomainEvent(ZoneId zoneId, UserId userId)
        {
            ZoneId = zoneId;
            UserId = userId;
        }
    }
}