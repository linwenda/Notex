using MediatR;

namespace SmartNote.Domain
{
    public interface IDomainEvent : INotification
    {
        Guid Id { get; }
        DateTime OccurredOn { get; }
    }
}