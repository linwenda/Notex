namespace Notex.Messages.MergeRequests.Events;

public class MergeRequestCreatedEvent : VersionedEvent
{
    public Guid CreatorId { get; }
    public Guid SourceNoteId { get; }
    public Guid DestinationNoteId { get; }
    public string Title { get; }
    public string Description { get; }

    public MergeRequestCreatedEvent(Guid sourcedId, int version, Guid creatorId, Guid sourceNoteId,
        Guid destinationNoteId, string title, string description) : base(sourcedId, version)
    {
        CreatorId = creatorId;
        SourceNoteId = sourceNoteId;
        DestinationNoteId = destinationNoteId;
        Title = title;
        Description = description;
    }
}