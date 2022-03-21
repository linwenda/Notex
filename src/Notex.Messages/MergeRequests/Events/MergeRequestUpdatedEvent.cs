using System;

namespace Notex.Messages.MergeRequests.Events;

public class MergeRequestUpdatedEvent : VersionedEvent
{
    public Guid UserId { get; }
    public string Title { get; }
    public string Description { get; }

    public MergeRequestUpdatedEvent(Guid aggregateId, int aggregateVersion, Guid userId, string title,
        string description) : base(aggregateId, aggregateVersion)
    {
        UserId = userId;
        Title = title;
        Description = description;
    }
}