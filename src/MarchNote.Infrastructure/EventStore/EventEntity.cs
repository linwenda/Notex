using System;

namespace MarchNote.Infrastructure.EventStore
{
    public class EventEntity
    {
        public Guid EntityId { get; set; }
        public int EntityVersion { get; set; }
        public DateTime Timestamp { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
    }
}