using System;

namespace Notex.Messages.Users.Events;

public class UserCreatedEvent : VersionedEvent
{
    public string UserName { get; }
    public string Password { get; }
    public string Email { get; }
    public string Name { get; }
    public bool IsActive { get; }

    public UserCreatedEvent(Guid aggregateId, int aggregateVersion, string userName, string password, string email,
        string name, bool isActive) : base(aggregateId, aggregateVersion)
    {
        UserName = userName;
        Password = password;
        Email = email;
        Name = name;
        IsActive = isActive;
    }
}