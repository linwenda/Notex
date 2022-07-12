using Notex.Core.Domain.SeedWork;
using Notex.Messages.Shared;
using Notex.Messages.Spaces;

namespace Notex.Core.Domain.Spaces;

public class SpaceMemento : IMemento
{
    public SpaceMemento(Guid sourcedId, int version, string name, string backgroundImage, Guid creatorId,
        Visibility visibility, bool isDeleted)
    {
        SourcedId = sourcedId;
        Version = version;
        Name = name;
        BackgroundImage = backgroundImage;
        CreatorId = creatorId;
        Visibility = visibility;
        IsDeleted = isDeleted;
    }

    public Guid SourcedId { get; }
    public int Version { get; }
    public string Name { get; }
    public string BackgroundImage { get; }
    public Guid CreatorId { get; }
    public Visibility Visibility { get; }
    public bool IsDeleted { get; }
}