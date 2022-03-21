using System.Collections.ObjectModel;
using Notex.Messages;

namespace Notex.Core.Aggregates;

public abstract class AggregateRoot
{
    public Guid Id { get; }
    public int Version { get; protected set; } = -1;

    private readonly ICollection<IVersionedEvent> _uncommittedEvents = new Collection<IVersionedEvent>();

    protected AggregateRoot(Guid id)
    {
        Id = id;
    }

    public IReadOnlyCollection<IVersionedEvent> PopUncommittedEvents()
    {
        var events = _uncommittedEvents.ToList();

        _uncommittedEvents.Clear();

        return events;
    }

    public void LoadFrom(IEnumerable<IVersionedEvent> history)
    {
        foreach (var e in history)
        {
            Apply(e);
            Version = e.AggregateVersion;
        }
    }

    protected void ApplyChange(IVersionedEvent @event)
    {
        Apply(@event);
        _uncommittedEvents.Add(@event);
    }

    protected abstract void Apply(IVersionedEvent @event);

    protected int GetNextVersion()
    {
        return Version + 1;
    }
}