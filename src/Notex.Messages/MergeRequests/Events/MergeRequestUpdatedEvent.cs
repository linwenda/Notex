namespace Notex.Messages.MergeRequests.Events;

public class MergeRequestUpdatedEvent : VersionedEvent
{
    public Guid UserId { get; }
    public string Title { get; }
    public string Description { get; }

    public MergeRequestUpdatedEvent(Guid sourcedId, int version, Guid userId, string title,
        string description) : base(sourcedId, version)
    {
        UserId = userId;
        Title = title;
        Description = description;
    }
}