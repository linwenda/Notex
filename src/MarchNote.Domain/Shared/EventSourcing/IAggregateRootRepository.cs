using System.Threading.Tasks;

namespace MarchNote.Domain.Shared.EventSourcing
{
    public interface IAggregateRootRepository<TAggregateRoot, in TAggregateIdentity>
        where TAggregateRoot : IAggregateRoot<TAggregateIdentity>
        where TAggregateIdentity : IAggregateIdentity
    {
        Task<TAggregateRoot> LoadAsync(TAggregateIdentity aggregateId, int version = int.MaxValue);
        Task SaveAsync(TAggregateRoot aggregate);
    }
}