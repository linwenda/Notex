namespace Notex.Messages.Comments.Events;

public class CommentEditedEvent : VersionedEvent
{
    public string Text { get; }

    public CommentEditedEvent(Guid sourcedId, int version, string text) : base(sourcedId, version)
    {
        Text = text;
    }
}