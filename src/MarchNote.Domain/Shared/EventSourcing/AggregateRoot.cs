using System.Collections.Generic;
using System.Linq;

namespace MarchNote.Domain.Shared.EventSourcing
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

            if (snapshot.EntityId != Id.Value)
            {
                throw new AggregateRootException($"Snapshot EntityId must be equal to {Id.Value}");
            }

            if (snapshot.EntityVersion != Version)
            {
                throw new AggregateRootException($"Snapshot EntityVersion must be equal to {Version}");
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
            //Implemented
            throw new AggregateRootException("Not implemented");
        }

        protected virtual ISnapshot CreateSnapshot()
        {
            //Implemented
            throw new AggregateRootException("Not implemented");
        }
    }
}