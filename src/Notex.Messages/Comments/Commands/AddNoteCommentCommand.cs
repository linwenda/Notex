namespace Notex.Messages.Comments.Commands;

public class AddNoteCommentCommand : ICommand<Guid>
{
    public Guid NoteId { get; set; }
    public string Text { get; set; }
}