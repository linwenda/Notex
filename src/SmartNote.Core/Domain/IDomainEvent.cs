using MediatR;

namespace SmartNote.Core.Domain;

public interface IDomainEvent : INotification
{
    Guid Id { get; }
    DateTime OccurredTime { get; }
}