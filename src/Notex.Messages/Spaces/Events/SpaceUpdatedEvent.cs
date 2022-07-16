using Notex.Messages.Shared;

namespace Notex.Messages.Spaces.Events;

public class SpaceUpdatedEvent : VersionedEvent
{
    public string Name { get; }
    public string Cover { get; }
    public Visibility Visibility { get; }

    public SpaceUpdatedEvent(
        Guid sourcedId,
        int version,
        string name,
        string backgroundImage,
        Visibility visibility) : base(sourcedId, version)
    {
        Name = name;
        Cover = backgroundImage;
        Visibility = visibility;
    }
}