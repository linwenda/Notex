namespace Notex.Messages.Notes.Commands;

public class PublishNoteCommand : ICommand
{
    public Guid NoteId { get; }

    public PublishNoteCommand(Guid noteId)
    {
        NoteId = noteId;
    }
}