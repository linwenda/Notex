using Notex.Messages.MergeRequests;
using Notex.Messages.MergeRequests.Events;

namespace Notex.Core.Aggregates.MergeRequests.ReadModel;

public class MergeRequestDetail: IReadModelEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid SourceNoteId { get; set; }
    public Guid DestinationNoteId { get; set; }
    public Guid? ReviewerId { get; set; }
    public DateTimeOffset? ReviewTime { get; set; }
    public MergeRequestStatus Status { get; set; }
    public Guid CreatorId { get; set; }
    public DateTimeOffset CreationTime { get; set; }
    public Guid? LastModifierId { get; set; }
    public DateTimeOffset? LastModificationTime { get; set; }

    public void When(MergeRequestCreatedEvent @event)
    {
        Id = @event.AggregateId;
        Title = @event.Title;
        Description = @event.Description;
        SourceNoteId = @event.SourceNoteId;
        DestinationNoteId = @event.DestinationNoteId;
        Status = MergeRequestStatus.Open;
        CreatorId = @event.CreatorId;
        CreationTime = @event.OccurrenceTime;
    }

    public void When(MergeRequestUpdatedEvent @event)
    {
        Title = @event.Title;
        Description = @event.Description;
        LastModifierId = @event.UserId;
        LastModificationTime = @event.OccurrenceTime;
    }

    public void When(MergeRequestClosedEvent @event)
    {
        Status = MergeRequestStatus.Closed;
        ReviewerId = @event.ReviewerId;
    }

    public void When(MergeRequestMergedEvent @event)
    {
        Status = MergeRequestStatus.Merged;
        ReviewerId = @event.ReviewerId;
    }

    public void When(MergeRequestReopenedEvent @event)
    {
        Status = MergeRequestStatus.Open;
        LastModifierId = @event.UserId;
        LastModificationTime = @event.OccurrenceTime;
    }
}