namespace Notex.Messages.Comments.Commands;

public class DeleteCommentCommand : ICommand
{
    public Guid CommentId { get; }

    public DeleteCommentCommand(Guid commentId)
    {
        CommentId = commentId;
    }
}