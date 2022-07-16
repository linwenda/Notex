using Notex.Messages.Shared;

namespace Notex.Messages.Spaces.Events;

public class SpaceCreatedEvent : VersionedEvent
{
    public Guid UserId { get; }
    public string Name { get; }
    public string Cover { get; }
    public Visibility Visibility { get; }

    public SpaceCreatedEvent(
        Guid sourcedId,
        int version,
        Guid userId,
        string name,
        string cover,
        Visibility visibility) : base(sourcedId, version)
    {
        UserId = userId;
        Name = name;
        Cover = cover;
        Visibility = visibility;
    }
}