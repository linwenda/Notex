using System;

namespace Notex.Messages.MergeRequests.Events;

public class MergeRequestCreatedEvent : VersionedEvent
{
    public Guid CreatorId { get; }
    public Guid SourceNoteId { get; }
    public Guid DestinationNoteId { get; }
    public string Title { get; }
    public string Description { get; }

    public MergeRequestCreatedEvent(Guid aggregateId, int aggregateVersion, Guid creatorId, Guid sourceNoteId,
        Guid destinationNoteId,
        string title, string description) : base(aggregateId, aggregateVersion)
    {
        CreatorId = creatorId;
        SourceNoteId = sourceNoteId;
        DestinationNoteId = destinationNoteId;
        Title = title;
        Description = description;
    }
}