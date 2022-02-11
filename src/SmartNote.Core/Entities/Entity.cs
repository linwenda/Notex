namespace SmartNote.Core.Entities;

public abstract class Entity : IEntity
{
    public abstract object[] GetKeys();
}

public abstract class Entity<TKey> : Entity, IEntity<TKey>
{
    public virtual TKey Id { get; protected set; }

    protected Entity()
    {

    }
    
    protected Entity(TKey id)
    {
        Id = id;
    }

    public override object[] GetKeys()
    {
        return new object[] {Id};
    }
}