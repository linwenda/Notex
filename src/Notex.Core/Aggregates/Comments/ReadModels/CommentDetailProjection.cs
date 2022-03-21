using Notex.Core.Configuration;
using Notex.Messages.Comments.Events;

namespace Notex.Core.Aggregates.Comments.ReadModels;

public class CommentDetailProjection :
    IEventHandler<CommentCreatedEvent>,
    IEventHandler<CommentEditedEvent>,
    IEventHandler<CommentDeletedEvent>
{
    private readonly IReadModelRepository _readModelRepository;

    public CommentDetailProjection(IReadModelRepository readModelRepository)
    {
        _readModelRepository = readModelRepository;
    }

    public async Task Handle(CommentCreatedEvent notification, CancellationToken cancellationToken)
    {
        var comment = new CommentDetail();

        comment.When(notification);

        await _readModelRepository.InsertAsync(comment);
    }

    public async Task Handle(CommentEditedEvent notification, CancellationToken cancellationToken)
    {
        var comment = await _readModelRepository.GetAsync<CommentDetail>(notification.AggregateId);

        comment.When(notification);

        await _readModelRepository.UpdateAsync(comment);
    }

    public async Task Handle(CommentDeletedEvent notification, CancellationToken cancellationToken)
    {
        var comment = await _readModelRepository.GetAsync<CommentDetail>(notification.AggregateId);

        comment.When(notification);

        await _readModelRepository.UpdateAsync(comment);
    }
}