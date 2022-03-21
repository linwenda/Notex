using System;

namespace Notex.Messages.Notes.Events;

public class NoteRestoredEvent : VersionedEvent
{
    public Guid UserId { get; }
    public string Title { get; }
    public string Content { get; }
    public int HistoryVersion { get; }

    public NoteRestoredEvent(Guid aggregateId, int aggregateVersion, Guid userId, string title, string content,
        int historyVersion) : base(aggregateId, aggregateVersion)
    {
        UserId = userId;
        Title = title;
        Content = content;
        HistoryVersion = historyVersion;
    }
}