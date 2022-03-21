using System;

namespace Notex.Messages;

public interface IVersionedEvent : IEvent
{
    public Guid AggregateId { get; }
    public int AggregateVersion { get; }
}

public abstract class VersionedEvent : IVersionedEvent
{
    protected VersionedEvent(Guid aggregateId, int aggregateVersion)
    {
        AggregateId = aggregateId;
        AggregateVersion = aggregateVersion;
        OccurrenceTime = DateTimeOffset.UtcNow;
    }

    public Guid AggregateId { get; }
    public int AggregateVersion { get; }
    public DateTimeOffset OccurrenceTime { get; }
}