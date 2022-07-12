namespace Notex.Messages.Spaces.Events;

public class SpaceDeletedEvent : VersionedEvent
{
    public SpaceDeletedEvent(Guid sourcedId, int version) : base(sourcedId, version)
    {
    }
}