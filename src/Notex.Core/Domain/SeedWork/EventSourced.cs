using System.Collections.ObjectModel;
using Notex.Messages;

namespace Notex.Core.Domain.SeedWork;

public abstract class EventSourced : IEventSourced
{
    public Guid Id { get; }
    public int Version { get; protected set; } = 0;

    private readonly ICollection<IVersionedEvent> _uncommittedEvents = new Collection<IVersionedEvent>();

    public IReadOnlyCollection<IVersionedEvent> PopUncommittedEvents()
    {
        var events = _uncommittedEvents.ToList();

        _uncommittedEvents.Clear();

        return events;
    }

    protected EventSourced(Guid id)
    {
        Id = id;
    }

    public void LoadFrom(IEnumerable<IVersionedEvent> history)
    {
        foreach (var e in history)
        {
            Apply(e);
            Version = e.Version;
        }
    }

    protected void ApplyChange(IVersionedEvent @event)
    {
        Apply(@event);
        Version = @event.Version;
        _uncommittedEvents.Add(@event);
    }

    protected abstract void Apply(IVersionedEvent @event);

    protected int GetNextVersion()
    {
        return Version + 1;
    }
}