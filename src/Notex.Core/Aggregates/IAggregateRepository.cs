namespace Notex.Core.Aggregates;

public interface IAggregateRepository
{
    Task<TAggregateRoot> LoadAsync<TAggregateRoot>(Guid id) where TAggregateRoot : AggregateRoot;
    Task SaveAsync<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot;
}