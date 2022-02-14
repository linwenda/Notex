namespace SmartNote.Core.Ddd;

public interface IReadOnlyRepository<out TEntity> where TEntity : class, IEntity
{
    IQueryable<TEntity> Queryable { get; }
}

public interface IReadOnlyRepository<out TEntity, TKey> : IReadOnlyRepository<TEntity> where TEntity : class, IEntity<TKey>
{
}