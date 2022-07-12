namespace Notex.Messages.Notes.Commands;

public class DeleteNoteCommand : ICommand
{
    public Guid NoteId { get; }

    public DeleteNoteCommand(Guid noteId)
    {
        NoteId = noteId;
    }
}