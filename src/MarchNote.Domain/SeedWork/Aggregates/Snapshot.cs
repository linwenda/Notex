using System;

namespace MarchNote.Domain.SeedWork.Aggregates
{
    public abstract class Snapshot : ISnapshot
    {
        public Guid AggregateId { get; }
        public int AggregateVersion { get; }

        protected Snapshot(Guid aggregateId, int aggregateVersion)
        {
            AggregateId = aggregateId;
            AggregateVersion = aggregateVersion;
        }
    }
}