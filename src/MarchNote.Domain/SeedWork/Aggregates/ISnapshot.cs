using System;

namespace MarchNote.Domain.SeedWork.Aggregates
{
    public interface ISnapshot
    {
        Guid AggregateId { get; }
        int AggregateVersion { get; }
    }
}