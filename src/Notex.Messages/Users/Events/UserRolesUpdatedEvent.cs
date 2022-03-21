using System;
using System.Collections.Generic;

namespace Notex.Messages.Users.Events;

public class UserRolesUpdatedEvent : VersionedEvent
{
    public ICollection<string> Roles { get; }

    public UserRolesUpdatedEvent(Guid aggregateId, int aggregateVersion, ICollection<string> roles) : base(aggregateId,
        aggregateVersion)
    {
        Roles = roles;
    }
}