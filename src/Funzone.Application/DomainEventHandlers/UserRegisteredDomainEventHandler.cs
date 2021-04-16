using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.Users.Events;
using MediatR;

namespace Funzone.Application.DomainEventHandlers
{
    public class UserRegisteredDomainEventHandler : INotificationHandler<UserRegisteredDomainEvent>
    {
        public Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}