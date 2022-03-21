using Notex.Core.Aggregates.Notes;
using Notex.Core.Aggregates.Spaces.DomainServices;
using Notex.Core.Aggregates.Spaces.Exceptions;
using Notex.Messages;
using Notex.Messages.Notes;
using Notex.Messages.Shared;
using Notex.Messages.Spaces.Events;

namespace Notex.Core.Aggregates.Spaces;

public class Space : AggregateRoot, IMementoOriginator
{
    private Guid _creatorId;
    private string _name;
    private string _backgroundImage;
    private Visibility _visibility;
    private bool _isDeleted;

    private Space(Guid id) : base(id)
    {
    }

    public static Space Initialize(ISpaceChecker spaceValidatorService, Guid userId, string name,
        string backgroundImage, Visibility visibility)
    {
        if (!spaceValidatorService.IsUniqueNameInUserSpace(userId, name))
        {
            throw new SpaceNameAlreadyExistsException();
        }

        var space = new Space(Guid.NewGuid());

        space.ApplyChange(new SpaceCreatedEvent(space.Id, space.GetNextVersion(), userId, name, backgroundImage,
            visibility));

        return space;
    }

    public void Update(ISpaceChecker validator, string name, string backgroundImage, Visibility visibility)
    {
        if (_isDeleted)
        {
            throw new SpaceHasBeenDeletedException();
        }

        if (_name != name)
        {
            if (!validator.IsUniqueNameInUserSpace(_creatorId, name))
            {
                throw new SpaceNameAlreadyExistsException();
            }
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

    protected override void Apply(IVersionedEvent @event)
    {
        this.When((dynamic) @event);
    }

    private void When(SpaceCreatedEvent @event)
    {
        _creatorId = @event.CreatorId;
        _name = @event.Name;
        _backgroundImage = @event.BackgroundImage;
        _visibility = @event.Visibility;
    }

    private void When(SpaceUpdatedEvent @event)
    {
        _name = @event.Name;
        _backgroundImage = @event.BackgroundImage;
        _visibility = @event.Visibility;
    }

    private void When(SpaceDeletedEvent @event)
    {
        _isDeleted = true;
    }

    public IMemento GetMemento()
    {
        return new SpaceMemento(Id, Version, _name, _backgroundImage, _creatorId, _visibility, _isDeleted);
    }

    public void SetMemento(IMemento memento)
    {
        var state = (SpaceMemento) memento;

        _creatorId = state.CreatorId;
        _name = state.Name;
        _backgroundImage = state.BackgroundImage;
        _visibility = state.Visibility;
        _isDeleted = state.IsDeleted;

        Version = state.AggregateVersion;
    }
}