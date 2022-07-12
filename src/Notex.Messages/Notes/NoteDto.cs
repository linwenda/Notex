using Notex.Messages.Shared;
using Notex.Messages.Spaces;

namespace Notex.Messages.Notes;

public class NoteDto
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
    public DateTime CreationTime { get; set; }
    public Guid? LastModifierId { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public bool IsDeleted { get; set; }
}