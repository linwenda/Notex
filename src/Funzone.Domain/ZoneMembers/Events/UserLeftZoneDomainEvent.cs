using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;

namespace Funzone.Domain.ZoneMembers.Events
{
    public class UserLeftZoneDomainEvent : DomainEventBase
    {
        public ZoneId ZoneId { get; }
        public UserId UserId { get; }

        public UserLeftZoneDomainEvent(ZoneId zoneId, UserId userId)
        {
            ZoneId = zoneId;
            UserId = userId;
        }
    }
}