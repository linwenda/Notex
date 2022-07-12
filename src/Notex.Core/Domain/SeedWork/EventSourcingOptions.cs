namespace Notex.Core.Domain.SeedWork;

public class EventSourcingOptions
{
    public int TakeEachSnapshotVersion { get; set; } = 5;
}