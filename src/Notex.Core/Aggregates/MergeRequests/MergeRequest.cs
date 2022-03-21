using Notex.Core.Aggregates.Comments;
using Notex.Core.Aggregates.MergeRequests.DomainServices;
using Notex.Core.Aggregates.MergeRequests.Exceptions;
using Notex.Core.Aggregates.Notes.DomainServices;
using Notex.Core.Aggregates.Notes.Exceptions;
using Notex.Core.Aggregates.Shared;
using Notex.Messages;
using Notex.Messages.MergeRequests;
using Notex.Messages.MergeRequests.Events;

namespace Notex.Core.Aggregates.MergeRequests;

public class MergeRequest : AggregateRoot
{
    private Guid _creatorId;
    private Guid _sourceNoteId;
    private Guid _destinationNoteId;
    private string _title;
    private string _description;
    private Guid? _reviewerId;
    private MergeRequestStatus _status;

    private MergeRequest(Guid id) : base(id)
    {
    }

    internal static MergeRequest Initialize(IMergeRequestChecker mergeRequestValidatorService, Guid userId,
        Guid sourceNoteId, Guid destinationNoteId, string title, string description)
    {
        if (mergeRequestValidatorService.HasOpenMergeRequest(sourceNoteId, destinationNoteId))
        {
            throw new MergeRequestHasBeenExistsException();
        }

        var mergeRequest = new MergeRequest(Guid.NewGuid());

        mergeRequest.ApplyChange(new MergeRequestCreatedEvent(mergeRequest.Id, mergeRequest.GetNextVersion(), userId,
            sourceNoteId, destinationNoteId, title, description));

        return mergeRequest;
    }

    public void Update(Guid userId, string title, string description)
    {
        if (_status != MergeRequestStatus.Open)
        {
            throw new OnlyOpenMergeRequestCanBeUpdatedException();
        }

        ApplyChange(new MergeRequestUpdatedEvent(Id, GetNextVersion(), userId, title, description));
    }

    public void Close(Guid userId)
    {
        if (_status != MergeRequestStatus.Open)
        {
            throw new MergeRequestStatusChangeException(_status, MergeRequestStatus.Closed);
        }

        ApplyChange(new MergeRequestClosedEvent(Id, GetNextVersion(), userId));
    }

    public void Reopen(INoteChecker noteValidatorService, Guid userId)
    {
        if (_status != MergeRequestStatus.Closed)
        {
            throw new MergeRequestStatusChangeException(_status, MergeRequestStatus.Open);
        }

        if (!noteValidatorService.IsPublishedNote(_destinationNoteId))
        {
            throw new NoteHaveNotBeenPublishedException();
        }

        ApplyChange(new MergeRequestReopenedEvent(Id, GetNextVersion(), userId));
    }

    public void Merge(INoteChecker noteValidatorService, Guid userId)
    {
        if (_status != MergeRequestStatus.Open)
        {
            throw new MergeRequestStatusChangeException(_status, MergeRequestStatus.Merged);
        }

        if (!noteValidatorService.IsPublishedNote(_sourceNoteId) ||
            !noteValidatorService.IsPublishedNote(_destinationNoteId))
        {
            throw new NoteHaveNotBeenPublishedException();
        }

        ApplyChange(new MergeRequestMergedEvent(Id, GetNextVersion(), userId, _sourceNoteId, _destinationNoteId));
    }

    public Comment AddComment(Guid userId, string text)
    {
        return Comment.Initialize(userId, new TargetEntity(nameof(MergeRequest), Id.ToString()), text);
    }

    protected override void Apply(IVersionedEvent @event)
    {
        this.When((dynamic) @event);
    }

    private void When(MergeRequestCreatedEvent @event)
    {
        _creatorId = @event.CreatorId;
        _sourceNoteId = @event.SourceNoteId;
        _destinationNoteId = @event.DestinationNoteId;
        _title = @event.Title;
        _description = @event.Title;
        _status = MergeRequestStatus.Open;
    }

    private void When(MergeRequestUpdatedEvent @event)
    {
        _title = @event.Title;
        _description = @event.Description;
    }

    private void When(MergeRequestClosedEvent @event)
    {
        _status = MergeRequestStatus.Closed;
        _reviewerId = @event.ReviewerId;
    }

    private void When(MergeRequestMergedEvent @event)
    {
        _status = MergeRequestStatus.Merged;
        _reviewerId = @event.ReviewerId;
    }

    private void When(MergeRequestReopenedEvent @event)
    {
        _status = MergeRequestStatus.Open;
    }
}