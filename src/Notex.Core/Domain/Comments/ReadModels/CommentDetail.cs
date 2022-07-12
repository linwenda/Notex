using Notex.Core.Domain.SeedWork;

namespace Notex.Core.Domain.Comments.ReadModels;

public class CommentDetail : IEntity
{
    public Guid Id { get; set; }
    public string EntityType { get; set; }
    public string EntityId { get; set; }
    public string Text { get; set; }
    public Guid? RepliedCommentId { get; set; }
    public Guid CreatorId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public bool IsDeleted { get; set; }
}