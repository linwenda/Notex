namespace Notex.Messages.Comments.Events;

public class CommentDeletedEvent : VersionedEvent
{
    public CommentDeletedEvent(Guid sourcedId, int version) : base(sourcedId, version)
    {
    }
}