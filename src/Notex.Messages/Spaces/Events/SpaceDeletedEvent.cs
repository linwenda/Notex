using System;

namespace Notex.Messages.Spaces.Events;

public class SpaceDeletedEvent : VersionedEvent
{
    public SpaceDeletedEvent(Guid aggregateId, int aggregateVersion) : base(aggregateId, aggregateVersion)
    {
    }
}