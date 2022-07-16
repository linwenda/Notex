using Notex.Core.Domain.Notes;
using Notex.Core.Domain.SeedWork;
using Notex.Core.Domain.Spaces.Exceptions;
using Notex.Messages;
using Notex.Messages.Notes;
using Notex.Messages.Shared;
using Notex.Messages.Spaces.Events;

namespace Notex.Core.Domain.Spaces;

public class Space : EventSourced, IMementoOriginator
{
    private Guid _creatorId;
    private string _name;
    private string _cover;
    private Visibility _visibility;
    private bool _isDeleted;

    private Space(Guid id) : base(id)
    {
    }

    internal static Space Initialize(Guid userId, string name, string backgroundImage, Visibility visibility)
    {
        var space = new Space(Guid.NewGuid());

        space.ApplyChange(new SpaceCreatedEvent(space.Id, space.GetNextVersion(), userId, name, backgroundImage,
            visibility));

        return space;
    }

    public void Update(string name, string backgroundImage, Visibility visibility)
    {
        if (_isDeleted)
        {
            throw new SpaceHasBeenDeletedException();
        }

        ApplyChange(new SpaceUpdatedEvent(
            Id,
            GetNextVersion(),
            name,
            backgroundImage,
            visibility));
    }

    public void Delete()
    {
        if (!_isDeleted)
        {
            ApplyChange(new SpaceDeletedEvent(Id, GetNextVersion()));
        }
    }

    public Note CreateNote(string title, string content, NoteStatus status)
    {
        if (_isDeleted)
        {
            throw new SpaceHasBeenDeletedException();
        }

        return Note.Initialize(Id, _creatorId, title, content, status);
    }

    public Guid GetCreatorId()
    {
        return _creatorId;
    }

    internal string GetName()
    {
        return _name;
    }

    protected override void Apply(IVersionedEvent @event)
    {
        this.When((dynamic)@event);
    }

    private void When(SpaceCreatedEvent @event)
    {
        _creatorId = @event.UserId;
        _name = @event.Name;
        _cover = @event.Cover;
        _visibility = @event.Visibility;
    }

    private void When(SpaceUpdatedEvent @event)
    {
        _name = @event.Name;
        _cover = @event.Cover;
        _visibility = @event.Visibility;
    }

    private void When(SpaceDeletedEvent @event)
    {
        _isDeleted = true;
    }

    public IMemento GetMemento()
    {
        return new SpaceMemento(Id, Version, _name, _cover, _creatorId, _visibility, _isDeleted);
    }

    public void SetMemento(IMemento memento)
    {
        var state = (SpaceMemento)memento;

        _creatorId = state.CreatorId;
        _name = state.Name;
        _cover = state.Cover;
        _visibility = state.Visibility;
        _isDeleted = state.IsDeleted;

        Version = state.Version;
    }
}