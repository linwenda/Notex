namespace SmartNote.Core.Domain;

public class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey>
{
    protected AggregateRoot()
    {
        
    }

    protected AggregateRoot(TKey id) : base(id)
    {
    }
    
    private List<IDomainEvent> _domainEvents;

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= new List<IDomainEvent>();
        _domainEvents.Add(domainEvent);
    }
}