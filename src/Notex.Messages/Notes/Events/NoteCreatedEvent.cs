using System;
using Notex.Messages.Shared;

namespace Notex.Messages.Notes.Events;

public class NoteCreatedEvent : VersionedEvent
{
    public Guid SpaceId { get; }
    public Guid CreatorId { get; }
    public string Title { get; }
    public string Content { get; }
    public NoteStatus Status { get; }
    public Visibility Visibility { get; }
    public Guid? CloneFormId { get; }

    public NoteCreatedEvent(Guid aggregateId, int aggregateVersion, Guid spaceId, Guid creatorId, string title,
        string content, NoteStatus status, Visibility visibility, Guid? cloneFormId = null) : base(aggregateId,
        aggregateVersion)
    {
        SpaceId = spaceId;
        CreatorId = creatorId;
        Title = title;
        Content = content;
        Status = status;
        Visibility = visibility;
        CloneFormId = cloneFormId;
    }
}