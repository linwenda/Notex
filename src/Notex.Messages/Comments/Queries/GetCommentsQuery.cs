namespace Notex.Messages.Comments.Queries;

public class GetCommentsQuery : IQuery<IEnumerable<CommentDto>>
{
    public string EntityType { get; set; }
    public string EntityId { get; set; }
}