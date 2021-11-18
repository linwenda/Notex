using System;
using MediatR;

namespace MarchNote.Domain.Shared
{
    public interface IDomainEvent : INotification
    {
        Guid Id { get; }
        DateTime OccurredOn { get; }
    }
}