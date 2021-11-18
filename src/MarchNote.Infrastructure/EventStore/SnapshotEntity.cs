using System;

namespace MarchNote.Infrastructure.EventStore
{
    public class SnapshotEntity
    {
        public Guid AggregateId { get; set; }
        public int AggregateVersion { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
    }
}