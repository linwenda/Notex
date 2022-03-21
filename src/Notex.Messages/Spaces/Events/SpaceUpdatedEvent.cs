using System;
using Notex.Messages.Shared;

namespace Notex.Messages.Spaces.Events;

public class SpaceUpdatedEvent : VersionedEvent
{
    public string Name { get; }
    public string BackgroundImage { get; }
    public Visibility Visibility { get; }

    public SpaceUpdatedEvent(
        Guid aggregateId,
        int aggregateVersion,
        string name,
        string backgroundImage,
        Visibility visibility) : base(aggregateId, aggregateVersion)
    {
        Name = name;
        BackgroundImage = backgroundImage;
        Visibility = visibility;
    }
}