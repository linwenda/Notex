namespace Notex.Messages.Notes.Events;

public class NoteTagsUpdatedEvent : VersionedEvent
{
    public ICollection<string> Tags { get; }
    public NoteTagsUpdatedEvent(Guid sourcedId, int version, ICollection<string> tags) : base(sourcedId,
        version)
    {
        Tags = tags;
    }
}