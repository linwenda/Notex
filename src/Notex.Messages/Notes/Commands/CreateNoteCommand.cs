namespace Notex.Messages.Notes.Commands;

public class CreateNoteCommand : ICommand<Guid>
{
    public Guid SpaceId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public NoteStatus Status { get; set; }
}