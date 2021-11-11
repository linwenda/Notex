using System.Threading.Tasks;

namespace MarchNote.Domain.Shared.EventSourcing
{
    public interface IEventSourcedRepository<TEntity, in TEntityId>
        where TEntity : IEventSourcedEntity<TEntityId>
        where TEntityId : IAggregateIdentity
    {
        Task<TEntity> LoadAsync(TEntityId aggregateId, int version = int.MaxValue);

        Task SaveAsync(TEntity aggregate);
    }
}