using Notex.Messages.Shared;

namespace Notex.Core.Aggregates.Spaces;

public class SpaceMemento : IMemento
{
    public SpaceMemento(Guid aggregateId, int aggregateVersion, string name, string backgroundImage, Guid creatorId,
        Visibility visibility, bool isDeleted)
    {
        AggregateId = aggregateId;
        AggregateVersion = aggregateVersion;
        Name = name;
        BackgroundImage = backgroundImage;
        CreatorId = creatorId;
        Visibility = visibility;
        IsDeleted = isDeleted;
    }

    public Guid AggregateId { get; }
    public int AggregateVersion { get; }
    public string Name { get; }
    public string BackgroundImage { get; }
    public Guid CreatorId { get; }
    public Visibility Visibility { get; }
    public bool IsDeleted { get; }
}