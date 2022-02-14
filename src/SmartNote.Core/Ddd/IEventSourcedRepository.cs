namespace SmartNote.Core.Ddd;

public interface IEventSourcedRepository<TAggregateRoot, in TAggregateKey>
    where TAggregateRoot : IEventSourcedAggregateRoot<TAggregateKey>
    where TAggregateKey : IEventSourcedAggregateKey
{
    Task<TAggregateRoot> LoadAsync(TAggregateKey aggregateRootId, int version = int.MaxValue);
    Task SaveAsync(TAggregateRoot aggregateRoot);
}