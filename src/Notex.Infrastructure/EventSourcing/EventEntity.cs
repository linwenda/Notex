namespace Notex.Infrastructure.EventSourcing;

public class EventEntity
{
    public Guid SourcedId { get; set; }
    public int Version { get; set; }
    public DateTime CreationTime { get; set; }
    public string Payload { get; set; }
    public string Type { get; set; }
}