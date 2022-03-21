using System;

namespace Notex.Messages.Notes.Events;

public class NoteMergedEvent : VersionedEvent
{
    public Guid SourceNoteId { get; }
    public string Title { get; }
    public string Content { get; }

    public NoteMergedEvent(Guid aggregateId, int aggregateVersion, Guid sourceNoteId, string title, string content) :
        base(aggregateId, aggregateVersion)
    {
        SourceNoteId = sourceNoteId;
        Title = title;
        Content = content;
    }
}