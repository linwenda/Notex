namespace Notex.Messages.MergeRequests.Events;

public class MergeRequestClosedEvent : VersionedEvent
{
    public Guid ReviewerId { get; }

    public MergeRequestClosedEvent(Guid sourcedId, int version, Guid reviewerId) : base(sourcedId, version)
    {
        ReviewerId = reviewerId;
    }
}