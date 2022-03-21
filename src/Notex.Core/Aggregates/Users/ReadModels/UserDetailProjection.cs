using Notex.Core.Configuration;
using Notex.Messages.Users.Events;

namespace Notex.Core.Aggregates.Users.ReadModels;

public class UserDetailProjection :
    IEventHandler<UserCreatedEvent>,
    IEventHandler<UserPasswordChangedEvent>,
    IEventHandler<UserProfileUpdatedEvent>,
    IEventHandler<UserRolesUpdatedEvent>
{
    private readonly IReadModelRepository _readModelRepository;

    public UserDetailProjection(IReadModelRepository readModelRepository)
    {
        _readModelRepository = readModelRepository;
    }

    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        var user = new UserDetail();

        user.When(notification);

        await _readModelRepository.InsertAsync(user);
    }

    public async Task Handle(UserPasswordChangedEvent notification, CancellationToken cancellationToken)
    {
        var user = await _readModelRepository.GetAsync<UserDetail>(notification.AggregateId);

        user.When(notification);

        await _readModelRepository.UpdateAsync(user);
    }

    public async Task Handle(UserProfileUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var user = await _readModelRepository.GetAsync<UserDetail>(notification.AggregateId);

        user.When(notification);

        await _readModelRepository.UpdateAsync(user);
    }

    public async Task Handle(UserRolesUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var user = await _readModelRepository.GetAsync<UserDetail>(notification.AggregateId);

        user.When(notification);

        await _readModelRepository.UpdateAsync(user);
    }
}