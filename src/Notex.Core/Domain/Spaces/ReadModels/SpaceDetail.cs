using Notex.Core.Domain.SeedWork;
using Notex.Messages.Shared;
using Notex.Messages.Spaces;

namespace Notex.Core.Domain.Spaces.ReadModels;

public class SpaceDetail : IEntity, ISoftDelete
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string BackgroundImage { get; set; }
    public Visibility Visibility { get; set; }
    public Guid CreatorId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public bool IsDeleted { get; set; }
}