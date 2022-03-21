using Notex.Messages.Notes;
using Notex.Messages.Notes.Events;
using Notex.Messages.Shared;

namespace Notex.Core.Aggregates.Notes.ReadModels;

public class NoteDetail : IReadModelEntity, ISoftDelete
{
    public Guid Id { get; set; }
    public Guid SpaceId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public NoteStatus Status { get; set; }
    public Visibility Visibility { get; set; }
    public Guid? CloneFormId { get; set; }
    public int Version { get; set; }
    public int ReadCount { get; set; }
    public string[] Tags { get; set; }
    public Guid CreatorId { get; set; }
    public DateTimeOffset CreationTime { get; set; }
    public Guid? LastModifierId { get; set; }
    public DateTimeOffset? LastModificationTime { get; set; }
    public bool IsDeleted { get; set; }

    public void When(NoteCreatedEvent @event)
    {
        Id = @event.AggregateId;
        Content = @event.Content;
        Status = @event.Status;
        Title = @event.Title;
        Visibility = @event.Visibility;
        CreatorId = @event.CreatorId;
        CreationTime = @event.OccurrenceTime;
        SpaceId = @event.SpaceId;
        CloneFormId = @event.CloneFormId;
        Version = Status == NoteStatus.Draft ? 0 : 1;
    }

    public void When(NoteEditedEvent @event)
    {
        Title = @event.Title;
        Content = @event.Content;
        LastModifierId = @event.UserId;
        LastModificationTime = @event.OccurrenceTime;

        if (Status == NoteStatus.Published)
        {
            Version += 1;
        }
    }

    public void When(NoteDeletedEvent @event)
    {
        IsDeleted = true;
    }

    public void When(NotePublishedEvent @event)
    {
        Status = NoteStatus.Published;
        Version += 1;
    }

    public void When(NoteRestoredEvent @event)
    {
        Title = @event.Title;
        Content = @event.Content;
        Version += 1;
    }

    public void When(NoteMergedEvent @event)
    {
        Title = @event.Title;
        Content = @event.Content;
        Version += 1;
    }

    public void When(NoteTagsUpdatedEvent @event)
    {
        Tags = @event.Tags.ToArray();
    }

    public void When(NoteVisibilityChangedEvent @event)
    {
        Visibility = @event.Visibility;
    }
}