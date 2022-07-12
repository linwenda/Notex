namespace Notex.Core.Domain.SeedWork;

public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}