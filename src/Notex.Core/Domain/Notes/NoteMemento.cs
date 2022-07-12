using Notex.Core.Domain.SeedWork;
using Notex.Messages.Notes;
using Notex.Messages.Shared;
using Notex.Messages.Spaces;

namespace Notex.Core.Domain.Notes;

public class NoteMemento : IMemento
{
    public Guid SourcedId { get; }
    public int Version { get; }
    public Guid SpaceId { get; }
    public Guid CreatorId { get; }
    public string Title { get; }
    public string Content { get; }
    public NoteStatus Status { get; }
    public Visibility Visibility { get; }
    public bool IsDeleted { get; }
    public ICollection<string> Tags { get; }
    public Guid? CloneFormId { get; }

    public NoteMemento(Guid sourcedId, int version, Guid spaceId, Guid creatorId, string title,
        string content, NoteStatus status, Visibility visibility, ICollection<string> tags,
        Guid? cloneFormId, bool isDeleted)
    {
        SourcedId = sourcedId;
        Version = version;
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