using Notex.Core.Aggregates.Comments.Exceptions;
using Notex.Core.Aggregates.Shared;
using Notex.Messages;
using Notex.Messages.Comments.Events;

namespace Notex.Core.Aggregates.Comments;

public class Comment : AggregateRoot
{
    private Guid _creatorId;
    private TargetEntity _targetEntity;
    private string _text;
    private Guid? _repliedCommentId;
    private bool _isDeleted;

    private Comment(Guid id) : base(id)
    {
    }

    internal static Comment Initialize(Guid userId, TargetEntity targetEntity, string text,
        Guid? repliedCommentId = null)
    {
        var comment = new Comment(Guid.NewGuid());

        comment.ApplyChange(new CommentCreatedEvent(comment.Id, comment.GetNextVersion(), userId, targetEntity.Type,
            targetEntity.Id, text, repliedCommentId));

        return comment;
    }

    public void Edit(string text)
    {
        CheckDelete();

        ApplyChange(new CommentEditedEvent(Id, GetNextVersion(), text));
    }

    public void Delete()
    {
        CheckDelete();

        ApplyChange(new CommentDeletedEvent(Id, GetNextVersion()));
    }

    public Comment Reply(Guid userId, string text)
    {
        CheckDelete();

        return Initialize(userId, _targetEntity, text, Id);
    }

    public Guid GetCreatorId()
    {
        return _creatorId;
    }

    private void CheckDelete()
    {
        if (_isDeleted)
        {
            throw new CommentHasBeenDeletedException();
        }
    }

    protected override void Apply(IVersionedEvent @event)
    {
        this.When((dynamic) @event);
    }

    private void When(CommentCreatedEvent @event)
    {
        _creatorId = @event.CreatorId;
        _targetEntity = new TargetEntity(@event.EntityType, @event.EntityId);
        _text = @event.Text;
        _repliedCommentId = @event.RepliedCommentId;
    }

    private void When(CommentEditedEvent @event)
    {
        _text = @event.Text;
    }

    private void When(CommentDeletedEvent @event)
    {
        _isDeleted = true;
    }
}