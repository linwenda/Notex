namespace Notex.Messages;

public interface IVersionedEvent : IEvent
{
    public Guid SourcedId { get; }
    public int Version { get; }
}