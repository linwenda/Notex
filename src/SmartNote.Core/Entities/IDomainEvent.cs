using MediatR;

namespace SmartNote.Core.Entities;

public interface IDomainEvent : INotification
{
    Guid Id { get; }
    DateTime OccurredTime { get; }
}