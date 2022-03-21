using System;

namespace Notex.Messages.MergeRequests.Events;

public class MergeRequestReopenedEvent : VersionedEvent
{
    public Guid UserId { get; }

    public MergeRequestReopenedEvent(Guid aggregateId, int aggregateVersion, Guid userId) : base(aggregateId,
        aggregateVersion)
    {
        UserId = userId;
    }
}