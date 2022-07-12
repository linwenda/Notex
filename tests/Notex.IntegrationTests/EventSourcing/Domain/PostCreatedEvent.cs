using System;
using Notex.Messages;

namespace Notex.IntegrationTests.EventSourcing.Domain;

public class PostCreatedEvent : IVersionedEvent
{
    public PostCreatedEvent(Guid sourcedId, int version, string title, string content, Guid authorId)
    {
        SourcedId = sourcedId;
        Version = version;
        Title = title;
        Content = content;
        AuthorId = authorId;
    }

    public Guid SourcedId { get; }
    public int Version { get; }
    public string Title { get; }
    public string Content { get; }
    public Guid AuthorId { get; }
}