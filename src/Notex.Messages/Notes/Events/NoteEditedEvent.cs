using System;

namespace Notex.Messages.Notes.Events;

public class NoteEditedEvent : VersionedEvent
{
    public Guid UserId { get; }
    public string Title { get; }
    public string Content { get; }
    public NoteStatus Status { get; }
    public string Comment { get; }

    public NoteEditedEvent(Guid aggregateId, int aggregateVersion, Guid userId, string title, string content,
        NoteStatus status, string comment) :
        base(aggregateId, aggregateVersion)
    {
        UserId = userId;
        Title = title;
        Content = content;
        Status = status;
        Comment = comment;
    }
}