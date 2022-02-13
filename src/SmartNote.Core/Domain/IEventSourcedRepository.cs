namespace SmartNote.Core.Domain;

public interface IEventSourcedRepository<TEventSourcedAggregateRoot, in TKey>
    where TEventSourcedAggregateRoot : IEventSourcedAggregateRoot<TKey>
    where TKey : IEventSourcedAggregateKey
{
    Task<TEventSourcedAggregateRoot> LoadAsync(TKey aggregateRootId, int version = int.MaxValue);
    Task SaveAsync(TEventSourcedAggregateRoot aggregateRoot);
}