using System.Text.Json;

namespace Notex.Infrastructure.EntityFrameworkCore;

public class EventRecord
{
    public Guid AggregateId { get; set; }
    public int AggregateVersion { get; set; }
    public DateTimeOffset CreationTime { get; set; }
    public JsonDocument Payload { get; set; }
    public string Type { get; set; }
}