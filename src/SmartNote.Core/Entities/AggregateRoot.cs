namespace SmartNote.Core.Entities;

public class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey>
{
    protected AggregateRoot()
    {
        
    }

    protected AggregateRoot(TKey id) : base(id)
    {
    }
}