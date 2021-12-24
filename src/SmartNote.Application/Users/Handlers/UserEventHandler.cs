using MediatR;
using SmartNote.Domain.Users.Events;

namespace SmartNote.Application.Users.Handlers;

public class UserEventHandler : INotificationHandler<UserRegisteredEvent>
{
    public Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}