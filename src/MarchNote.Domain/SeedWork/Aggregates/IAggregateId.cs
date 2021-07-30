using System;

namespace MarchNote.Domain.SeedWork.Aggregates
{
    public interface IAggregateId
    {
        Guid Value { get; }
    }
}