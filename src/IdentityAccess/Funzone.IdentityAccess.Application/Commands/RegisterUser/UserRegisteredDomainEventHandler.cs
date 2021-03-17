using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Funzone.IdentityAccess.Application.IntegrationEvents.Events;
using Funzone.IdentityAccess.Domain.Users.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Funzone.IdentityAccess.Application.Commands.RegisterUser
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