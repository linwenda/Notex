using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Funzone.UserAccess.Domain.Users.Events;
using Funzone.UserAccess.IntegrationEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Funzone.UserAccess.Application.Users.RegisterUser
{
    public class UserRegisteredDomainEventHandler : INotificationHandler<UserRegisteredDomainEvent>
    {
        private readonly IEventBus _eventBus;

        public UserRegisteredDomainEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            await _eventBus.Publish(new UserRegisteredIntegrationEvent(
                notification.Id,
                notification.OccurredOn,
                notification.UserId.Value,
                notification.UserName,
                notification.Email));
        }
    }
}