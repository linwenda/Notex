namespace SmartNote.Core.Domain;

public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class, IEntity
{
}

public interface IRepository<TEntity, TKey> : IRepository<TEntity> where TEntity : class, IEntity<TKey>
{
}

