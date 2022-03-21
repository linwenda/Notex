using Notex.Core.Configuration;
using Notex.Messages.MergeRequests;
using Notex.Messages.MergeRequests.Events;

namespace Notex.Core.Aggregates.MergeRequests.ReadModel;

public class MergeRequestDetailProjection :
    IEventHandler<MergeRequestCreatedEvent>,
    IEventHandler<MergeRequestUpdatedEvent>,
    IEventHandler<MergeRequestClosedEvent>,
    IEventHandler<MergeRequestMergedEvent>,
    IEventHandler<MergeRequestReopenedEvent>
{
    private readonly IReadModelRepository _readModelRepository;

    public MergeRequestDetailProjection(IReadModelRepository readModelRepository)
    {
        _readModelRepository = readModelRepository;
    }

    public async Task Handle(MergeRequestCreatedEvent notification, CancellationToken cancellationToken)
    {
        var mergeRequest = new MergeRequestDetail();

        mergeRequest.When(notification);

        await _readModelRepository.InsertAsync(mergeRequest);
    }

    public async Task Handle(MergeRequestUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var mergeRequest = await _readModelRepository.GetAsync<MergeRequestDetail>(notification.AggregateId);

        mergeRequest.When(notification);

        await _readModelRepository.UpdateAsync(mergeRequest);
    }

    public async Task Handle(MergeRequestClosedEvent notification, CancellationToken cancellationToken)
    {
        var mergeRequest = await _readModelRepository.GetAsync<MergeRequestDetail>(notification.AggregateId);

        mergeRequest.When(notification);

        await _readModelRepository.UpdateAsync(mergeRequest);
    }

    public async Task Handle(MergeRequestMergedEvent notification, CancellationToken cancellationToken)
    {
        var mergeRequest = await _readModelRepository.GetAsync<MergeRequestDetail>(notification.AggregateId);

        mergeRequest.When(notification);

        await _readModelRepository.UpdateAsync(mergeRequest);
    }

    public async Task Handle(MergeRequestReopenedEvent notification, CancellationToken cancellationToken)
    {
        var mergeRequest = await _readModelRepository.GetAsync<MergeRequestDetail>(notification.AggregateId);

        mergeRequest.When(notification);

        await _readModelRepository.UpdateAsync(mergeRequest);
    }
}