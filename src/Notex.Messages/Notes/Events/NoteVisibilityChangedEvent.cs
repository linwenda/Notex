using Notex.Messages.Shared;
using Notex.Messages.Spaces;

namespace Notex.Messages.Notes.Events;

public class NoteVisibilityChangedEvent : VersionedEvent
{
    public Visibility Visibility { get; }

    public NoteVisibilityChangedEvent(Guid sourcedId, int version, Visibility visibility) : base(sourcedId,
        version)
    {
        Visibility = visibility;
    }
}