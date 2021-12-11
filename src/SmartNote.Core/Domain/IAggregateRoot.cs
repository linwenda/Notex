using System.Collections.Generic;

namespace SmartNote.Core.Domain
{
    public interface IAggregateRoot<out TIdentity> where TIdentity : IAggregateIdentity
    {
        TIdentity Id { get; }
        int Version { get; }
        IReadOnlyCollection<IDomainEvent> GetUnCommittedEvents();
        ISnapshot GetUnCommittedSnapshot();
        void Load(IEnumerable<IDomainEvent> history);
        void Load(ISnapshot snapshot, IEnumerable<IDomainEvent> history);
    }
}