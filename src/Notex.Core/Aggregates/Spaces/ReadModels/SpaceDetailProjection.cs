using Notex.Core.Configuration;
using Notex.Messages.Spaces.Events;

namespace Notex.Core.Aggregates.Spaces.ReadModels;

public class SpaceDetailProjection : IEventHandler<SpaceCreatedEvent>, IEventHandler<SpaceUpdatedEvent>,
    IEventHandler<SpaceDeletedEvent>
{
    private readonly IReadModelRepository _readModelRepository;

    public SpaceDetailProjection(IReadModelRepository readModelRepository)
    {
        _readModelRepository = readModelRepository;
    }
    
    public async Task Handle(SpaceCreatedEvent notification, CancellationToken cancellationToken)
    {
        var space = new SpaceDetail();

        space.When(notification);

        await _readModelRepository.InsertAsync(space);
    }

    public async Task Handle(SpaceUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var space = await _readModelRepository.GetAsync<SpaceDetail>(notification.AggregateId);

        space.When(notification);

        await _readModelRepository.UpdateAsync(space);
    }

    public async Task Handle(SpaceDeletedEvent notification, CancellationToken cancellationToken)
    {
        var space = await _readModelRepository.GetAsync<SpaceDetail>(notification.AggregateId);

        space.When(notification);

        await _readModelRepository.UpdateAsync(space);
    }
}