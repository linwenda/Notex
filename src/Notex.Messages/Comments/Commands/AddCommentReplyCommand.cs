namespace Notex.Messages.Comments.Commands;

public class AddCommentReplyCommand : ICommand<Guid>
{
    public Guid CommentId { get; }
    public string Text { get; }

    public AddCommentReplyCommand(Guid commentId, string text)
    {
        CommentId = commentId;
        Text = text;
    }
}