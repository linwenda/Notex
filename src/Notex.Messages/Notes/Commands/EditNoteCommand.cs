namespace Notex.Messages.Notes.Commands;

public class EditNoteCommand : ICommand
{
    public Guid NoteId { get; }
    public string Title { get; }
    public string Content { get; }
    public string Comment { get; }

    public EditNoteCommand(Guid noteId, string title, string content, string comment)
    {
        NoteId = noteId;
        Title = title;
        Content = content;
        Comment = comment;
    }
}