using Notex.Messages.Notes;
using Notex.Messages.Shared;

namespace Notex.Core.Aggregates.Notes;

public class NoteMemento : IMemento
{
    public Guid AggregateId { get; }
    public int AggregateVersion { get; }
    public Guid SpaceId { get; }
    public Guid CreatorId { get; }
    public string Title { get; }
    public string Content { get; }
    public NoteStatus Status { get; }
    public Visibility Visibility { get; }
    public bool IsDeleted { get; }
    public ICollection<string> Tags { get; }
    public Guid? CloneFormId { get; }

    public NoteMemento(Guid aggregateId, int aggregateVersion, Guid spaceId, Guid creatorId, string title,
        string content, NoteStatus status, Visibility visibility, ICollection<string> tags,
        Guid? cloneFormId, bool isDeleted)
    {
        AggregateId = aggregateId;
        AggregateVersion = aggregateVersion;
        SpaceId = spaceId;
        CreatorId = creatorId;
        Title = title;
        Content = content;
        Status = status;
        Visibility = visibility;
        IsDeleted = isDeleted;
        Tags = tags;
        CloneFormId = cloneFormId;
    }
}