using System;
using Notex.Messages.Shared;

namespace Notex.Messages.Spaces.Events;

public class SpaceCreatedEvent : VersionedEvent
{
    public Guid CreatorId { get; }
    public string Name { get; }
    public string BackgroundImage { get; }
    public Visibility Visibility { get; }

    public SpaceCreatedEvent(
        Guid aggregateId,
        int aggregateVersion,
        Guid creatorId,
        string name,
        string backgroundImage,
        Visibility visibility) : base(aggregateId, aggregateVersion)
    {
        CreatorId = creatorId;
        Name = name;
        BackgroundImage = backgroundImage;
        Visibility = visibility;
    }
}