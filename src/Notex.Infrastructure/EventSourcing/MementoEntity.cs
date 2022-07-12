namespace Notex.Infrastructure.EventSourcing;

public class MementoEntity
{
    public Guid SourcedId { get; set; }
    public int Version { get; set; }
    public string Payload { get; set; }
    public string Type { get; set; }
}