using System;

namespace Notex.Messages.Comments.Events;

public class CommentDeletedEvent : VersionedEvent
{
    public CommentDeletedEvent(Guid aggregateId, int aggregateVersion) : base(aggregateId, aggregateVersion)
    {
    }
}