namespace SmartNote.Core.Domain;

public interface IEventSourcedAggregateRoot<out TKey> : IAggregateRoot<TKey> where TKey : IEventSourcedAggregateKey
{
    int Version { get; }
    IReadOnlyCollection<IDomainEvent> GetUnCommittedEvents();
    ISnapshot<TKey> GetUnCommittedSnapshot();
    void Load(IEnumerable<IDomainEvent> history);
    void Load(ISnapshot snapshot, IEnumerable<IDomainEvent> history);
}