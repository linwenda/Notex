using Notex.Core.Domain.SeedWork;

namespace Notex.Core.Domain.Notes.ReadModels;

public class NoteHistory : IEntity
{
    public Guid Id { get; set; }
    public Guid NoteId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid? CloneFromId { get; set; }
    public int Version { get; set; }
    public string Comment { get; set; }
    public DateTime CreationTime { get; set; }
}