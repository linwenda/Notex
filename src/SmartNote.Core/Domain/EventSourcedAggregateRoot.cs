namespace SmartNote.Core.Domain;

public abstract class EventSourcedAggregateRoot<TKey> : IEventSourcedAggregateRoot<TKey>
    where TKey : IEventSourcedAggregateKey
{
    public TKey Id { get; }
    public int Version { get; private set; } = 0;

    private readonly List<IDomainEvent> _unCommittedEvents = new();

    private ISnapshot<TKey> _unCommittedSnapshot;

    protected EventSourcedAggregateRoot(TKey id)
    {
        Id = id;
    }

    public IReadOnlyCollection<IDomainEvent> GetUnCommittedEvents()
    {
        var events = _unCommittedEvents.ToList();

        _unCommittedEvents.Clear();

        return events;
    }

    ISnapshot<TKey> IEventSourcedAggregateRoot<TKey>.GetUnCommittedSnapshot()
    {
        throw new NotImplementedException();
    }

    public ISnapshot GetUnCommittedSnapshot()
    {
        var snapshot = _unCommittedSnapshot;

        _unCommittedSnapshot = null;

        return snapshot;
    }

    public void Load(IEnumerable<IDomainEvent> history)
    {
        foreach (var e in history)
        {
            Apply(e);
            Version++;
        }
    }

    public void Load(ISnapshot snapshot, IEnumerable<IDomainEvent> history)
    {
        throw new NotImplementedException();
    }


    public void Load(ISnapshot<TKey> snapshot, IEnumerable<IDomainEvent> history)
    {
        LoadSnapshot(snapshot);
        Load(history);
    }

    public void TakeSnapshot()
    {
        var snapshot = CreateSnapshot();

        if (snapshot.AggregateRootId.Value != Id.Value)
        {
            throw new EventSourcedAggregateRootException($"Snapshot AggregateId must be equal to {Id.Value}");
        }

        if (snapshot.AggregateRootVersion != Version)
        {
            throw new EventSourcedAggregateRootException($"Snapshot AggregateVersion must be equal to {Version}");
        }

        _unCommittedSnapshot = snapshot;
    }

    protected void ApplyChange(IDomainEvent @event)
    {
        Apply(@event);
        _unCommittedEvents.Add(@event);
    }

    protected abstract void Apply(IDomainEvent @event);

    protected virtual void LoadSnapshot(ISnapshot snapshot)
    {
        throw new NotImplementedException();
    }

    protected virtual ISnapshot<TKey> CreateSnapshot()
    {
        throw new NotImplementedException();
    }

    public object[] GetKeys()
    {
        throw new NotImplementedException();
    }
}