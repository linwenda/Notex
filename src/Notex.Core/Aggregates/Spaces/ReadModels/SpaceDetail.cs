using Notex.Messages.Shared;
using Notex.Messages.Spaces.Events;

namespace Notex.Core.Aggregates.Spaces.ReadModels;

public class SpaceDetail : IReadModelEntity, ISoftDelete
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string BackgroundImage { get; set; }
    public Visibility Visibility { get; set; }
    public Guid CreatorId { get; set; }
    public DateTimeOffset CreationTime { get; set; }
    public DateTimeOffset? LastModificationTime { get; set; }
    public bool IsDeleted { get; set; }

    public void When(SpaceCreatedEvent @event)
    {
        Id = @event.AggregateId;
        Name = @event.Name;
        Visibility = @event.Visibility;
        CreatorId = @event.CreatorId;
        BackgroundImage = @event.BackgroundImage;
        CreationTime = @event.OccurrenceTime;
    }

    public void When(SpaceUpdatedEvent @event)
    {
        Name = @event.Name;
        Visibility = @event.Visibility;
        BackgroundImage = @event.BackgroundImage;
        LastModificationTime = @event.OccurrenceTime;
    }

    public void When(SpaceDeletedEvent @event)
    {
        IsDeleted = true;
    }
}