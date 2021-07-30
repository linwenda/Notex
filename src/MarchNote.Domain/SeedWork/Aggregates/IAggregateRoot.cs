using System.Collections.Generic;

namespace MarchNote.Domain.SeedWork.Aggregates
{
    public interface IAggregateRoot<out TIdentity> where TIdentity : IAggregateId
    {
        TIdentity Id { get; }
        int Version { get; }
        IReadOnlyCollection<IDomainEvent> GetUnCommittedEvents();
        ISnapshot GetUnCommittedSnapshot();
        void Load(IEnumerable<IDomainEvent> history);
        void Load(ISnapshot snapshot,IEnumerable<IDomainEvent> history);
    }
}