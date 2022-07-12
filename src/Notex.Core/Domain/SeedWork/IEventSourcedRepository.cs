namespace Notex.Core.Domain.SeedWork;

public interface IEventSourcedRepository<T> where T : IEventSourced
{
    Task<T> FindAsync(Guid id, CancellationToken cancellationToken = default);
    Task<T> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task SaveAsync(T eventSourced, CancellationToken cancellationToken = default);
}