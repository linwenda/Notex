using Notex.Core.Aggregates.Comments;
using Notex.Core.Aggregates.MergeRequests;
using Notex.Core.Aggregates.MergeRequests.DomainServices;
using Notex.Core.Aggregates.Notes.DomainServices;
using Notex.Core.Aggregates.Notes.Exceptions;
using Notex.Core.Aggregates.Shared;
using Notex.Messages;
using Notex.Messages.Notes;
using Notex.Messages.Notes.Events;
using Notex.Messages.Shared;

namespace Notex.Core.Aggregates.Notes;

public class Note : AggregateRoot, IMementoOriginator
{
    private Guid _spaceId;
    private Guid _creatorId;
    private string _title;
    private string _content;
    private NoteStatus _status;
    private Visibility _visibility;
    private Guid? _cloneFormId;
    private ICollection<string> _tags;
    private bool _isDeleted;

    private Note(Guid id) : base(id)
    {
    }

    internal static Note Initialize(Guid spaceId, Guid userId, string title, string content, NoteStatus status,
        Guid? cloneFromId = null)
    {
        var note = new Note(Guid.NewGuid());

        note.ApplyChange(new NoteCreatedEvent(note.Id, note.GetNextVersion(), spaceId, userId, title, content, status,
            status == NoteStatus.Draft ? Visibility.Private : Visibility.Public, cloneFromId));

        return note;
    }

    public void Edit(Guid userId, string title, string content, string comment)
    {
        CheckDelete();

        ApplyChange(new NoteEditedEvent(Id, GetNextVersion(), userId, title, content, _status, comment));
    }

    public void Delete()
    {
        if (_isDeleted) return;

        ApplyChange(new NoteDeletedEvent(Id, GetNextVersion()));
    }

    public void Publish()
    {
        if (_status == NoteStatus.Published) return;

        CheckDelete();

        ApplyChange(new NotePublishedEvent(Id, GetNextVersion()));
    }

    public void ChangeVisibility(Visibility visibility)
    {
        if (_visibility == visibility || _status != NoteStatus.Published) return;

        CheckDelete();

        ApplyChange(new NoteVisibilityChangedEvent(Id, GetNextVersion(), visibility));
    }

    public void UpdateTags(ICollection<string> tags)
    {
        CheckDelete();

        var inFirstOnly = _tags.Except(tags);
        var inSecondOnly = tags.Except(_tags);

        if (!inFirstOnly.Any() && !inSecondOnly.Any()) return;

        ApplyChange(new NoteTagsUpdatedEvent(Id, GetNextVersion(), tags));
    }

    public void Restore(Guid userId, string title, string content, int historyVersion)
    {
        CheckPublish();

        ApplyChange(new NoteRestoredEvent(Id, GetNextVersion(), userId, title, content, historyVersion));
    }

    public void Merge(Guid sourceNoteId, string title, string content)
    {
        CheckPublish();

        ApplyChange(new NoteMergedEvent(Id, GetNextVersion(), sourceNoteId, title, content));
    }

    public Note Clone(Guid userId, Guid spaceId)
    {
        CheckPublish();

        return Initialize(spaceId, userId, _title, _content, NoteStatus.Published, Id);
    }

    public Comment AddComment(Guid userId, string text)
    {
        CheckPublish();

        return Comment.Initialize(userId, new TargetEntity(nameof(Note), Id.ToString()), text);
    }

    public MergeRequest CreateMergeRequest(INoteChecker noteChecker, IMergeRequestChecker mergeRequestChecker,
        Guid userId, string title, string description)
    {
        CheckDelete();

        if (!_cloneFormId.HasValue)
        {
            throw new OnlyCloneNoteCanBeMergedException();
        }

        if (!noteChecker.IsPublishedNote(_cloneFormId.Value))
        {
            throw new NoteHaveNotBeenPublishedException();
        }

        return MergeRequest.Initialize(mergeRequestChecker, userId, Id, _cloneFormId.Value, title, description);
    }

    public IMemento GetMemento()
    {
        return new NoteMemento(Id, Version, _spaceId, _creatorId, _title, _content, _status, _visibility, _tags,
            _cloneFormId, _isDeleted);
    }

    public void SetMemento(IMemento memento)
    {
        var state = (NoteMemento) memento;

        Version = state.AggregateVersion;

        _spaceId = state.SpaceId;
        _creatorId = state.CreatorId;
        _title = state.Title;
        _content = state.Content;
        _visibility = state.Visibility;
        _tags = state.Tags;
        _cloneFormId = state.CloneFormId;
        _isDeleted = state.IsDeleted;
    }

    private void CheckDelete()
    {
        if (_isDeleted)
        {
            throw new NoteHasBeenDeletedException();
        }
    }

    private void CheckPublish()
    {
        CheckDelete();

        if (_status != NoteStatus.Published)
        {
            throw new NoteHaveNotBeenPublishedException();
        }
    }

    protected override void Apply(IVersionedEvent @event)
    {
        this.When((dynamic) @event);
    }

    private void When(NoteCreatedEvent @event)
    {
        _spaceId = @event.SpaceId;
        _creatorId = @event.CreatorId;
        _title = @event.Title;
        _content = @event.Content;
        _status = @event.Status;
        _visibility = @event.Visibility;
        _cloneFormId = @event.CloneFormId;
        _tags = new List<string>();
    }

    private void When(NoteEditedEvent @event)
    {
        _title = @event.Title;
        _content = @event.Content;
    }

    private void When(NoteDeletedEvent @event)
    {
        _isDeleted = true;
    }

    private void When(NotePublishedEvent @event)
    {
        _status = NoteStatus.Published;
    }

    private void When(NoteVisibilityChangedEvent @event)
    {
        _visibility = @event.Visibility;
    }

    private void When(NoteTagsUpdatedEvent @event)
    {
        _tags = @event.Tags;
    }

    private void When(NoteMergedEvent @event)
    {
        _title = @event.Title;
        _content = @event.Content;
    }

    private void When(NoteRestoredEvent @event)
    {
        _title = @event.Title;
        _content = @event.Content;
    }
}