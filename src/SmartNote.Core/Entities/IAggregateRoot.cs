namespace SmartNote.Core.Entities;

public interface IAggregateRoot<out TKey> : IEntity<TKey>
{
}