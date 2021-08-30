using System;

namespace MarchNote.Infrastructure.Events
{
    public class SnapshotEntity
    {
        public Guid EntityId { get; set; }
        public int EntityVersion { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
    }
}