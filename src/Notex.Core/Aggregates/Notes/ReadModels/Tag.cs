namespace Notex.Core.Aggregates.Notes.ReadModels;

public class Tag
{
    public string Name { get; set; }
    public int UsageCount { get; set; }
    public DateTime CreationTime { get; set; }
}