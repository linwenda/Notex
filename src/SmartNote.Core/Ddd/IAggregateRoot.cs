namespace SmartNote.Core.Ddd;

public interface IAggregateRoot
{
}

public interface IAggregateRoot<out TKey> : IEntity<TKey>, IAggregateRoot
{
}