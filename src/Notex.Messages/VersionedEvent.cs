namespace Notex.Messages;

public abstract class VersionedEvent : IVersionedEvent
{
    protected VersionedEvent(Guid sourcedId, int version)
    {
        SourcedId = sourcedId;
        Version = version;
    }

    public Guid SourcedId { get; }
    public int Version { get; }
}