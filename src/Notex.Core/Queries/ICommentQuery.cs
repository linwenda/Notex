using Notex.Core.Aggregates.Comments.ReadModels;

namespace Notex.Core.Queries;

public interface ICommentQuery
{
    Task<CommentDetail> GetCommentAsync(Guid id);
    Task<IEnumerable<CommentDetail>> GetCommentsAsync(string entityType, string entityId);
}