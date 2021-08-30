using System.Threading.Tasks;

namespace MarchNote.Domain.SeedWork.EventSourcing
{
    public interface IEventSourcedRepository<TEntity, in TEntityId>
        where TEntity : IEventSourcedEntity<TEntityId>
        where TEntityId : TypedIdValueBase
    {
        Task<TEntity> LoadAsync(TEntityId aggregateId, int version = int.MaxValue);

        Task SaveAsync(TEntity aggregate);
    }
}