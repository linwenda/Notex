using Notex.Core.Domain.MergeRequests.ReadModels;
using Notex.Core.Domain.SeedWork;
using Notex.Messages.MergeRequests;
using Notex.Messages.MergeRequests.Events;

namespace Notex.Core.Handlers.ReadModelGenerators;

public class MergeRequestReadModelGenerator :
    IEventHandler<MergeRequestCreatedEvent>,
    IEventHandler<MergeRequestUpdatedEvent>,
    IEventHandler<MergeRequestClosedEvent>,
    IEventHandler<MergeRequestMergedEvent>,
    IEventHandler<MergeRequestReopenedEvent>
{
    private readonly IRepository<MergeRequestDetail> _repository;

    public MergeRequestReadModelGenerator(IRepository<MergeRequestDetail> repository)
    {
        _repository = repository;
    }

    public async Task Handle(MergeRequestCreatedEvent notification, CancellationToken cancellationToken)
    {
        var mergeRequest = new MergeRequestDetail
        {
            Id = notification.SourcedId,
            Title = notification.Title,
            Description = notification.Description,
            SourceNoteId = notification.SourceNoteId,
            DestinationNoteId = notification.DestinationNoteId,
            Status = MergeRequestStatus.Open,
            CreatorId = notification.CreatorId,
            CreationTime = DateTime.Now
        };

        await _repository.InsertAsync(mergeRequest, cancellationToken);
    }

    public async Task Handle(MergeRequestUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var mergeRequest = await _repository.GetAsync(notification.SourcedId, cancellationToken);

        mergeRequest.Title = notification.Title;
        mergeRequest.Description = notification.Description;
        mergeRequest.LastModifierId = notification.UserId;
        mergeRequest.LastModificationTime = DateTime.Now;

        await _repository.UpdateAsync(mergeRequest, cancellationToken);
    }

    public async Task Handle(MergeRequestClosedEvent notification, CancellationToken cancellationToken)
    {
        var mergeRequest = await _repository.GetAsync(notification.SourcedId, cancellationToken);

        mergeRequest.Status = MergeRequestStatus.Closed;
        mergeRequest.ReviewerId = notification.ReviewerId;
        mergeRequest.ReviewTime = DateTime.Now;

        await _repository.UpdateAsync(mergeRequest, cancellationToken);
    }

    public async Task Handle(MergeRequestMergedEvent notification, CancellationToken cancellationToken)
    {
        var mergeRequest = await _repository.GetAsync(notification.SourcedId, cancellationToken);

        mergeRequest.Status = MergeRequestStatus.Merged;
        mergeRequest.ReviewerId = notification.ReviewerId;
        mergeRequest.ReviewTime = notification.OccurrenceTime;

        await _repository.UpdateAsync(mergeRequest, cancellationToken);
    }

    public async Task Handle(MergeRequestReopenedEvent notification, CancellationToken cancellationToken)
    {
        var mergeRequest = await _repository.GetAsync(notification.SourcedId, cancellationToken);

        mergeRequest.Status = MergeRequestStatus.Open;
        mergeRequest.LastModifierId = notification.UserId;
        mergeRequest.LastModificationTime = DateTime.Now;

        await _repository.UpdateAsync(mergeRequest, cancellationToken);
    }
}