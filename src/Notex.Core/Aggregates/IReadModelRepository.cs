namespace Notex.Core.Aggregates;

public interface IReadModelRepository
{
    Task<TEntity> GetAsync<TEntity>(object id) where TEntity : class, IReadModelEntity;
    IQueryable<TEntity> Query<TEntity>() where TEntity : class, IReadModelEntity;
    Task<TEntity> InsertAsync<TEntity>(TEntity entity) where TEntity : class, IReadModelEntity;
    Task<TEntity> UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IReadModelEntity;
    Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class, IReadModelEntity;
}

public interface IReadModelEntity
{
}