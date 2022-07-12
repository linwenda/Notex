using Notex.Core.Domain.Comments.ReadModels;
using Notex.Core.Domain.SeedWork;
using Notex.Messages.Comments.Events;

namespace Notex.Core.Handlers.ReadModelGenerators;

public class CommentReadModelGenerator :
    IEventHandler<CommentCreatedEvent>,
    IEventHandler<CommentEditedEvent>,
    IEventHandler<CommentDeletedEvent>
{
    private readonly IRepository<CommentDetail> _repository;

    public CommentReadModelGenerator(IRepository<CommentDetail> repository)
    {
        _repository = repository;
    }

    public async Task Handle(CommentCreatedEvent notification, CancellationToken cancellationToken)
    {
        var comment = new CommentDetail
        {
            Id = notification.SourcedId,
            CreatorId = notification.CreatorId,
            CreationTime = DateTime.Now,
            EntityType = notification.EntityType,
            EntityId = notification.EntityId,
            Text = notification.Text,
            RepliedCommentId = notification.RepliedCommentId,
            IsDeleted = false
        };

        await _repository.InsertAsync(comment, cancellationToken);
    }

    public async Task Handle(CommentEditedEvent notification, CancellationToken cancellationToken)
    {
        var comment = await _repository.GetAsync(notification.SourcedId, cancellationToken);

        comment.Text = notification.Text;
        comment.LastModificationTime = DateTime.Now;

        await _repository.UpdateAsync(comment, cancellationToken);
    }

    public async Task Handle(CommentDeletedEvent notification, CancellationToken cancellationToken)
    {
        var comment = await _repository.GetAsync(notification.SourcedId, cancellationToken);

        comment.IsDeleted = true;

        await _repository.UpdateAsync(comment, cancellationToken);
    }
}