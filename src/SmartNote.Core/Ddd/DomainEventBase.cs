namespace SmartNote.Core.Ddd;

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