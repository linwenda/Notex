using System;

namespace Notex.Messages.MergeRequests.Events;

public class MergeRequestClosedEvent : VersionedEvent
{
    public Guid ReviewerId { get; }

    public MergeRequestClosedEvent(Guid aggregateId, int aggregateVersion, Guid reviewerId) : base(aggregateId,
        aggregateVersion)
    {
        ReviewerId = reviewerId;
    }
}