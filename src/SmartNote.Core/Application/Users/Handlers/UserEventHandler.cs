using MediatR;
using SmartNote.Core.Domain.Users.Events;

namespace SmartNote.Core.Application.Users.Handlers;

public class UserEventHandler : INotificationHandler<UserRegisteredEvent>
{
    public Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}