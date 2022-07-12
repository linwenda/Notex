namespace Notex.Messages.Notes.Events;

public class NoteDeletedEvent : VersionedEvent
{
    public NoteDeletedEvent(Guid sourcedId, int version) : base(sourcedId, version)
    {
    }
}