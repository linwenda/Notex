namespace Notex.Messages.Comments.Commands;

public class EditCommentCommand : ICommand
{
    public Guid CommentId { get; }
    public string Text { get; }

    public EditCommentCommand(Guid commentId, string text)
    {
        CommentId = commentId;
        Text = text;
    }
}