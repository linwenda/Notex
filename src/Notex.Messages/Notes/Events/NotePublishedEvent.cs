using System;

namespace Notex.Messages.Notes.Events;

public class NotePublishedEvent : VersionedEvent
{
    public NotePublishedEvent(Guid aggregateId, int aggregateVersion) : base(aggregateId, aggregateVersion)
    {
    }
}