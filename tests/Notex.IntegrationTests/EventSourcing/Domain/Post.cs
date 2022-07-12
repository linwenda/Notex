using System;
using Notex.Core.Domain.SeedWork;
using Notex.Messages;

namespace Notex.IntegrationTests.EventSourcing.Domain;

public class Post : EventSourced, IMementoOriginator
{
    private Guid _authorId;
    private string _title;
    private string _content;

    private Post(Guid id) : base(id)
    {
    }

    public static Post Initialize(Guid authorId, string title, string content)
    {
        var post = new Post(Guid.NewGuid());

        post.ApplyChange(new PostCreatedEvent(post.Id, post.GetNextVersion(), title, content, authorId));

        return post;
    }

    public void Edit(string title, string content)
    {
        ApplyChange(new PostEditedEvent(Id, GetNextVersion(), title, content));
    }

    protected override void Apply(IVersionedEvent @event)
    {
        this.When((dynamic)@event);
    }

    private void When(PostCreatedEvent @event)
    {
        _authorId = @event.AuthorId;
        _title = @event.Title;
        _content = @event.Content;
    }

    private void When(PostEditedEvent @event)
    {
        _title = @event.Title;
        _content = @event.Content;
    }

    public IMemento GetMemento()
    {
        return new PostMemento(Id, Version, _title, _content, _authorId);
    }

    public void SetMemento(IMemento memento)
    {
        var state = (PostMemento)memento;

        _authorId = state.AuthorId;
        _title = state.Title;
        _content = state.Content;

        Version = state.Version;
    }
}