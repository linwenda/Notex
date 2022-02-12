namespace SmartNote.Core.Domain;

public interface IReadOnlyRepository<out TEntity> where TEntity : class, IEntity
{
    IQueryable<TEntity> Queryable { get; }
}

public interface IReadOnlyRepository<TEntity, TKey> : IReadOnlyRepository<TEntity> where TEntity : class, IEntity<TKey>
{
}