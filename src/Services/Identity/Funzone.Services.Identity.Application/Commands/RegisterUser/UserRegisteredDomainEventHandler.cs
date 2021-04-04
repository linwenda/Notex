using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Funzone.Services.Identity.Application.IntegrationEvents.Events;
using Funzone.Services.Identity.Domain.Users.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Funzone.Services.Identity.Application.Commands.RegisterUser
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
            //TODO: Use Outbox Pattern
            // await _eventBus.Publish(new UserRegisteredIntegrationEvent(
            //     notification.UserId.Value, 
            //     notification.UserName, 
            //     notification.Email));

            await Task.CompletedTask;
        }
    }
}