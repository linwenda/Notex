namespace Notex.Messages.MergeRequests.Events;

public class MergeRequestMergedEvent : VersionedEvent, IEvent
{
    public Guid ReviewerId { get; }
    public Guid SourceNoteId { get; }
    public Guid DestinationNoteId { get; }

    public MergeRequestMergedEvent(Guid sourcedId, int version, Guid reviewerId, Guid sourceNoteId,
        Guid destinationNoteId) : base(sourcedId, version)
    {
        ReviewerId = reviewerId;
        SourceNoteId = sourceNoteId;
        DestinationNoteId = destinationNoteId;
        OccurrenceTime = DateTime.Now;
    }

    public DateTime OccurrenceTime { get; }
}