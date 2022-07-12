using System;
using Notex.Core.Domain.SeedWork;

namespace Notex.IntegrationTests.EventSourcing.Domain;

public class PostMemento : IMemento
{
    public Guid SourcedId { get; }
    public int Version { get; }
    public string Title { get; }
    public string Content { get; }
    public Guid AuthorId { get; }

    public PostMemento(Guid sourcedId, int version, string title, string content, Guid authorId)
    {
        SourcedId = sourcedId;
        Version = version;
        Title = title;
        Content = content;
        AuthorId = authorId;
    }
}