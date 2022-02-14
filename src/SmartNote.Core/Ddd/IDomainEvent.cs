using MediatR;

namespace SmartNote.Core.Ddd;

public interface IDomainEvent : INotification
{
    Guid Id { get; }
    DateTime OccurredTime { get; }
}