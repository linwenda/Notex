using Notex.Core.Configuration;
using Notex.Messages.Users.Events;

namespace Notex.Core.Aggregates.Users.EventHandlers;

public class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
{
    private readonly IEmailSender _emailSender;

    public UserCreatedEventHandler(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        return _emailSender.SendAsync("to@email.com", "subject@email.com", "Welcome");
    }
}