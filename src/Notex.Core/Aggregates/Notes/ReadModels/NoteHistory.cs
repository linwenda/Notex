namespace Notex.Core.Aggregates.Notes.ReadModels;

public class NoteHistory : IReadModelEntity
{
    public Guid Id { get; set; }
    public Guid NoteId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid? CloneFormId { get; set; }
    public int Version { get; set; }
    public string Comment { get; set; }
    public DateTimeOffset CreationTime { get; set; }
}