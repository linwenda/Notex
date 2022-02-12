namespace SmartNote.Core.Domain;

public class DomainEventBase : IDomainEvent
{
    public Guid Id { get; }
    public DateTime OccurredTime { get; }

    protected DomainEventBase()
    {
        Id = Guid.NewGuid();
        OccurredTime = DateTime.UtcNow;
    }
}