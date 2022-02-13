using MediatR;
using SmartNote.Core.Domain.Users;
using SmartNote.Core.Services;

namespace SmartNote.Core.Application.DomainEventHandlers;

public class UserRegisteredEventHandler : INotificationHandler<UserRegisteredEvent>
{
    private readonly IEmailSender _emailSender;

    public UserRegisteredEventHandler(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
    {
        await _emailSender.SendAsync(notification.Email, "Hello");
    }
}