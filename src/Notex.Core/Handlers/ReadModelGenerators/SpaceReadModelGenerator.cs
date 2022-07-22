using Notex.Core.Domain.SeedWork;
using Notex.Core.Domain.Spaces.ReadModels;
using Notex.Messages.Spaces.Events;

namespace Notex.Core.Handlers.ReadModelGenerators;

public class SpaceReadModelGenerator :
    IEventHandler<SpaceCreatedEvent>,
    IEventHandler<SpaceUpdatedEvent>,
    IEventHandler<SpaceDeletedEvent>
{
    private readonly IRepository<SpaceDetail> _repository;

    public SpaceReadModelGenerator(IRepository<SpaceDetail> repository)
    {
        _repository = repository;
    }

    public async Task Handle(SpaceCreatedEvent notification, CancellationToken cancellationToken)
    {
        var space = new SpaceDetail
        {
            Id = notification.SourcedId,
            Name = notification.Name,
            Visibility = notification.Visibility,
            CreatorId = notification.UserId,
            Cover = notification.Cover,
            CreationTime = DateTime.UtcNow,
            LastModificationTime = DateTime.UtcNow
        };

        await _repository.InsertAsync(space, cancellationToken);
    }

    public async Task Handle(SpaceUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var space = await _repository.GetAsync(notification.SourcedId, cancellationToken);

        space.Name = notification.Name;
        space.Visibility = notification.Visibility;
        space.Cover = notification.Cover;
        space.LastModificationTime = DateTime.UtcNow;

        await _repository.UpdateAsync(space, cancellationToken);
    }

    public async Task Handle(SpaceDeletedEvent notification, CancellationToken cancellationToken)
    {
        var space = await _repository.GetAsync(notification.SourcedId, cancellationToken);

        space.IsDeleted = true;

        await _repository.UpdateAsync(space, cancellationToken);
    }
}