using System;

namespace Notex.Messages.Notes.Events;

public class NoteDeletedEvent : VersionedEvent
{
    public NoteDeletedEvent(Guid aggregateId, int aggregateVersion) : base(aggregateId, aggregateVersion)
    {
    }
}