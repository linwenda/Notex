using System;
using MediatR;

namespace Funzone.Domain.SeedWork
{
    public interface IDomainEvent : INotification
    {
        Guid Id { get; }

        DateTime OccurredOn { get; }
    }
}