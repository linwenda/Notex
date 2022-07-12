namespace Notex.Messages.Notes.Events;

public class NotePublishedEvent : VersionedEvent
{
    public NotePublishedEvent(Guid sourcedId, int version) : base(sourcedId, version)
    {
    }
}