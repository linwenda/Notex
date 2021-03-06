using System.Threading;
using System.Threading.Tasks;
using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Funzone.IdentityAccess.Domain.Users.Events;
using Funzone.IdentityAccess.IntegrationEvents;
using MediatR;

namespace Funzone.IdentityAccess.Application.Users.RegisterUser
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