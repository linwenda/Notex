namespace SmartNote.Core.Domain;

public interface IAggregateRoot
{
}

public interface IAggregateRoot<out TKey> : IEntity<TKey>, IAggregateRoot
{
}