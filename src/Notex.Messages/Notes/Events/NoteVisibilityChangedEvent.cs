using System;
using Notex.Messages.Shared;

namespace Notex.Messages.Notes.Events;

public class NoteVisibilityChangedEvent : VersionedEvent
{
    public Visibility Visibility { get; }

    public NoteVisibilityChangedEvent(Guid aggregateId, int aggregateVersion, Visibility visibility) : base(aggregateId,
        aggregateVersion)
    {
        Visibility = visibility;
    }
}