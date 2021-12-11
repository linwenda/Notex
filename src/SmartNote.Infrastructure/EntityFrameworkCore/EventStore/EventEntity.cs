namespace SmartNote.Infrastructure.EntityFrameworkCore.EventStore
{
    public class EventEntity
    {
        public Guid AggregateId { get; set; }
        public int AggregateVersion { get; set; }
        public DateTime Timestamp { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
    }
}