using System;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Zones.Events
{
    public class ZoneClosedDomainEvent : DomainEventBase
    {
        public ZoneClosedDomainEvent(ZoneId zoneId)
        {
            ZoneId = zoneId;
        }

        public ZoneId ZoneId { get; }
    }
}