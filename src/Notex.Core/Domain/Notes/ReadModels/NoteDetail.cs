using Newtonsoft.Json;
using Notex.Core.Domain.SeedWork;
using Notex.Messages.Notes;
using Notex.Messages.Notes.Events;
using Notex.Messages.Shared;
using Notex.Messages.Spaces;

namespace Notex.Core.Domain.Notes.ReadModels;

public class NoteDetail : IEntity, ISoftDelete
{
    public Guid Id { get; set; }
    public Guid SpaceId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public NoteStatus Status { get; set; }
    public Visibility Visibility { get; set; }
    public Guid? CloneFromId { get; set; }
    public int Version { get; set; }
    public int ReadCount { get; set; }
    public string[] Tags { get; set; }

    public string SerializeTags
    {
        get => JsonConvert.SerializeObject(Tags);
        set => Tags = JsonConvert.DeserializeObject<string[]>(value);
    }

    public Guid CreatorId { get; set; }
    public DateTime CreationTime { get; set; }
    public Guid? LastModifierId { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public bool IsDeleted { get; set; }


    public void When(NoteTagsUpdatedEvent @event)
    {
        Tags = @event.Tags.ToArray();
    }
}