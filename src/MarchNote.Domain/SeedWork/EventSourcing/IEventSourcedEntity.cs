using System.Collections.Generic;

namespace MarchNote.Domain.SeedWork.EventSourcing
{
    public interface IEventSourcedEntity<out TIdentity> where TIdentity : TypedIdValueBase
    {
        TIdentity Id { get; }
        int Version { get; }
        IReadOnlyCollection<IDomainEvent> GetUnCommittedEvents();
        ISnapshot GetUnCommittedSnapshot();
        void Load(IEnumerable<IDomainEvent> history);
        void Load(ISnapshot snapshot, IEnumerable<IDomainEvent> history);
    }
}