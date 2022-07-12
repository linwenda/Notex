namespace Notex.Messages.MergeRequests.Events;

public class MergeRequestReopenedEvent : VersionedEvent
{
    public Guid UserId { get; }

    public MergeRequestReopenedEvent(Guid sourcedId, int version, Guid userId) : base(sourcedId,
        version)
    {
        UserId = userId;
    }
}