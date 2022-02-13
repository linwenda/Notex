namespace SmartNote.Core.Domain
{
    public interface IReadModelEntity : IEntity
    {
    }

    public interface IReadModelEntity<out TKey> : IReadModelEntity, IEntity<TKey>
    {
    }
}