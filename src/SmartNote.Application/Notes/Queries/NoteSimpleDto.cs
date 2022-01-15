namespace SmartNote.Application.Notes.Queries;

public class NoteSimpleDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTimeOffset CreationTime { get; set; }
}