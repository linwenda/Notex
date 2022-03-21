using Notex.Messages.Comments.Events;

namespace Notex.Core.Aggregates.Comments.ReadModels;

public class CommentDetail : IReadModelEntity
{
    public Guid Id { get; set; }
    public string EntityType { get; set; }
    public string EntityId { get; set; }
    public string Text { get; set; }
    public Guid? RepliedCommentId { get; set; }
    public Guid CreatorId { get; set; }
    public DateTimeOffset CreationTime { get; set; }
    public DateTimeOffset? LastModificationTime { get; set; }
    public bool IsDeleted { get; set; }

    public void When(CommentCreatedEvent @event)
    {
        Id = @event.AggregateId;
        CreatorId = @event.CreatorId;
        CreationTime = @event.OccurrenceTime;
        EntityType = @event.EntityType;
        EntityId = @event.EntityId;
        Text = @event.Text;
        RepliedCommentId = @event.RepliedCommentId;
        IsDeleted = false;
    }

    public void When(CommentEditedEvent @event)
    {
        Text = @event.Text;
        LastModificationTime = @event.OccurrenceTime;
    }

    public void When(CommentDeletedEvent @event)
    {
        IsDeleted = true;
    }
}