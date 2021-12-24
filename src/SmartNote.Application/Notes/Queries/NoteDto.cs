using SmartNote.Domain.Notes;

namespace SmartNote.Application.Notes.Queries;

public class NoteDto
{
    public Guid Id { get; set; }
    public DateTimeOffset CreationTime { get; set; }
    public Guid? ForkId { get; set; }
    public Guid AuthorId { get; set; }
    public Guid SpaceId { get; set; }
    public string Title { get; set; }
    public int Version { get; set; }
    public NoteStatus Status { get; set; }
    public bool IsDeleted { get; set; }
    public List<BlockDto> Blocks { get; set; }
}