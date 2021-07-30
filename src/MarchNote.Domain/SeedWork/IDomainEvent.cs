using System;
using MediatR;

namespace MarchNote.Domain.SeedWork
{
    public interface IDomainEvent : INotification
    {
        Guid Id { get; }
        DateTime OccurredOn { get; }
    }
}