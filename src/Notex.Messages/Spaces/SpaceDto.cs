using Notex.Messages.Shared;

namespace Notex.Messages.Spaces;

public class SpaceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Cover { get; set; }
    public Visibility Visibility { get; set; }
    public Guid CreatorId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public bool IsDeleted { get; set; }
}