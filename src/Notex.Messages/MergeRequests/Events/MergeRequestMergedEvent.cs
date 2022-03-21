using System;

namespace Notex.Messages.MergeRequests.Events;

public class MergeRequestMergedEvent : VersionedEvent
{
    public Guid ReviewerId { get; }
    public Guid SourceNoteId { get; }
    public Guid DestinationNoteId { get; }

    public MergeRequestMergedEvent(Guid aggregateId, int aggregateVersion, Guid reviewerId, Guid sourceNoteId,
        Guid destinationNoteId) : base(aggregateId, aggregateVersion)
    {
        ReviewerId = reviewerId;
        SourceNoteId = sourceNoteId;
        DestinationNoteId = destinationNoteId;
    }
}