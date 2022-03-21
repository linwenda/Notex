using System;

namespace Notex.Messages.Users.Events;

public class UserProfileUpdatedEvent : VersionedEvent
{
    public string Name { get; }
    public string Bio { get; }
    public string Avatar { get; }

    public UserProfileUpdatedEvent(Guid aggregateId, int aggregateVersion, string name, string bio, string avatar) :
        base(aggregateId, aggregateVersion)
    {
        Name = name;
        Bio = bio;
        Avatar = avatar;
    }
}