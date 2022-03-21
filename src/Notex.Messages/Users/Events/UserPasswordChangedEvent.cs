using System;

namespace Notex.Messages.Users.Events;

public class UserPasswordChangedEvent : VersionedEvent
{
    public string Password { get; }

    public UserPasswordChangedEvent(Guid aggregateId, int aggregateVersion, string password) : base(aggregateId,
        aggregateVersion)
    {
        Password = password;
    }
}