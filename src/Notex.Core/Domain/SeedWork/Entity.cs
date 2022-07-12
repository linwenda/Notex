namespace Notex.Core.Domain.SeedWork;

public abstract class Entity : IEntity
{
}

public abstract class Entity<T> : Entity
{
    public T Id { get; set; }
}