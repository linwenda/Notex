namespace Notex.Messages.Comments.Events;

public class CommentCreatedEvent : VersionedEvent
{
    public Guid CreatorId { get; }
    public string EntityType { get; }
    public string EntityId { get; }
    public string Text { get; }
    public Guid? RepliedCommentId { get; }

    public CommentCreatedEvent(Guid sourcedId, int version, Guid creatorId, string entityType,
        string entityId, string text, Guid? repliedCommentId) : base(sourcedId, version)
    {
        CreatorId = creatorId;
        EntityType = entityType;
        EntityId = entityId;
        RepliedCommentId = repliedCommentId;
        Text = text;
    }
}