using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MarchNote.Domain.Users.Events;

namespace MarchNote.Application.Users.Handlers
{
    public class UserEventHandler : INotificationHandler<UserRegisteredEvent>
    {
        public Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}