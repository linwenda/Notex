namespace Notex.Messages.Comments.Queries;

public class GetCommentQuery : IQuery<CommentDto>
{
    public Guid CommentId { get; }

    public GetCommentQuery(Guid commentId)
    {
        CommentId = commentId;
    }
}