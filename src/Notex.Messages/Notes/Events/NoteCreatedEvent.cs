using Notex.Messages.Shared;
using Notex.Messages.Spaces;

namespace Notex.Messages.Notes.Events;

public class NoteCreatedEvent : VersionedEvent
{
    public Guid SpaceId { get; }
    public Guid CreatorId { get; }
    public string Title { get; }
    public string Content { get; }
    public NoteStatus Status { get; }
    public Visibility Visibility { get; }
    public Guid? CloneFromId { get; }

    public NoteCreatedEvent(Guid sourcedId, int version, Guid spaceId, Guid creatorId, string title,
        string content, NoteStatus status, Visibility visibility, Guid? cloneFromId = null) : base(sourcedId,
        version)
    {
        SpaceId = spaceId;
        CreatorId = creatorId;
        Title = title;
        Content = content;
        Status = status;
        Visibility = visibility;
        CloneFromId = cloneFromId;
    }
}