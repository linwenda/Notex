using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.Zones.Events
{
    public class ZoneCreatedDomainEvent : DomainEventBase
    {
        public Zone Zone { get; }

        public ZoneCreatedDomainEvent(Zone zone)
        {
            Zone = zone;
        }
    }
}