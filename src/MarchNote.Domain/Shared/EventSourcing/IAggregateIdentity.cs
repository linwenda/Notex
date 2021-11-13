using System;

namespace MarchNote.Domain.Shared.EventSourcing
{
    public interface IAggregateIdentity
    {
        Guid Value { get; }
    }
}