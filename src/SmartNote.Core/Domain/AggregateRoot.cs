namespace SmartNote.Core.Domain
{
    public abstract class AggregateRoot<TIdentity> : IAggregateRoot<TIdentity>
        where TIdentity : IAggregateIdentity
    {
        public TIdentity Id { get; }
        public int Version { get; private set; } = 0;

        private readonly List<IDomainEvent> _unCommittedEvents = new();

        private ISnapshot _unCommittedSnapshot;

        protected AggregateRoot(TIdentity id)
        {
            Id = id;
        }

        public IReadOnlyCollection<IDomainEvent> GetUnCommittedEvents()
        {
            var events = _unCommittedEvents.ToList();

            _unCommittedEvents.Clear();

            return events;
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
            LoadSnapshot(snapshot);
            Load(history);
        }

        public void TakeSnapshot()
        {
            var snapshot = CreateSnapshot();

            if (snapshot.AggregateId != Id.Value)
            {
                throw new AggregateRootException($"Snapshot AggregateId must be equal to {Id.Value}");
            }

            if (snapshot.AggregateVersion != Version)
            {
                throw new AggregateRootException($"Snapshot AggregateVersion must be equal to {Version}");
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
            throw new AggregateRootException("LoadSnapshot not implemented");
        }

        protected virtual ISnapshot CreateSnapshot()
        {
            throw new AggregateRootException("CreateSnapshot not implemented");
        }
    }
}