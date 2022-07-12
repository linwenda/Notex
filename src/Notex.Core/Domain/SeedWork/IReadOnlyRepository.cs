using System.Linq.Expressions;

namespace Notex.Core.Domain.SeedWork;

public interface IReadOnlyRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity> GetAsync(object id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    IQueryable<TEntity> Query();
}