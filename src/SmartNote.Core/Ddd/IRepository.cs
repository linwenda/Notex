using System.Linq.Expressions;

namespace SmartNote.Core.Ddd;

public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class, IEntity
{
    //Throw EntityNotFoundException if null
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IReadOnlyList<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate);
    Task InsertAsync(TEntity entity);
    Task InsertManyAsync(IEnumerable<TEntity> entities);
    Task UpdateAsync(TEntity entity);
    Task UpdateManyAsync(IEnumerable<TEntity> entities);
    Task DeleteAsync(TEntity entity);
    Task DeleteManyAsync(IEnumerable<TEntity> entities);
}

public interface IRepository<TEntity, in TKey> : IRepository<TEntity> where TEntity : class, IEntity<TKey>
{
    //Throw EntityNotFoundException if null
    Task<TEntity> GetAsync(TKey id);
    Task<TEntity> FindAsync(TKey id);
}