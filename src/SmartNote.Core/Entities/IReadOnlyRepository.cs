namespace SmartNote.Core.Entities;

public interface IReadOnlyRepository<TEntity> where TEntity : class, IEntity
{
    Task<IQueryable<TEntity>> GetQueryableAsync();
}

public interface IReadOnlyRepository<TEntity, TKey> : IReadOnlyRepository<TEntity> where TEntity : class, IEntity<TKey>
{
}