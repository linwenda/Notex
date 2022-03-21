using System;

namespace Notex.Messages.Comments.Events;

public class CommentEditedEvent : VersionedEvent
{
    public string Text { get; }

    public CommentEditedEvent(Guid aggregateId, int aggregateVersion, string text) : base(aggregateId, aggregateVersion)
    {
        Text = text;
    }
}