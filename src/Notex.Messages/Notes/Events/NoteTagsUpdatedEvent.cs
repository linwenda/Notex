using System;
using System.Collections.Generic;

namespace Notex.Messages.Notes.Events;

public class NoteTagsUpdatedEvent : VersionedEvent
{
    public ICollection<string> Tags { get; }

    public NoteTagsUpdatedEvent(Guid aggregateId, int aggregateVersion, ICollection<string> tags) : base(aggregateId,
        aggregateVersion)
    {
        Tags = tags;
    }
}