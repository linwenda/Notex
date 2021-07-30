using System.Threading;
using System.Threading.Tasks;

namespace MarchNote.Domain.SeedWork.Aggregates
{
    public interface IAggregateRepository<TAggregate, in TAggregateId>
        where TAggregate : IAggregateRoot<TAggregateId>
        where TAggregateId : IAggregateId
    {
        Task<TAggregate> LoadAsync(TAggregateId aggregateId,
            CancellationToken cancellationToken = default);

        Task<TAggregate> LoadAsync(TAggregateId aggregateId, int version,
            CancellationToken cancellationToken = default);

        Task SaveAsync(TAggregate aggregate, CancellationToken cancellationToken = default);
    }
}