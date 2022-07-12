namespace Notex.Core.Domain.Notes.ReadModels;

public class Tag
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int UsageCount { get; set; }
    public DateTime CreationTime { get; set; }
}