using Notex.Core.Domain.Comments;
using Notex.Core.Domain.MergeRequests.Exceptions;
using Notex.Core.Domain.SeedWork;
using Notex.Messages;
using Notex.Messages.MergeRequests;
using Notex.Messages.MergeRequests.Events;
using Notex.Messages.Shared;

namespace Notex.Core.Domain.MergeRequests;

public class MergeRequest : EventSourced
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

    public static MergeRequest Initialize(Guid userId, Guid sourceNoteId, Guid destinationNoteId,
        string title, string description)
    {
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

    public void ReOpen(Guid userId)
    {
        if (_status != MergeRequestStatus.Closed)
        {
            throw new MergeRequestStatusChangeException(_status, MergeRequestStatus.Open);
        }
        
        ApplyChange(new MergeRequestReopenedEvent(Id, GetNextVersion(), userId));
    }

    public void SetMergeStatus(Guid userId)
    {
        if (_status != MergeRequestStatus.Open)
        {
            throw new MergeRequestStatusChangeException(_status, MergeRequestStatus.Merged);
        }

        ApplyChange(new MergeRequestMergedEvent(Id, GetNextVersion(), userId, _sourceNoteId, _destinationNoteId));
    }

    public Comment AddComment(Guid userId, string text)
    {
        return Comment.Initialize(userId, new TargetEntity(nameof(MergeRequest), Id.ToString()), text);
    }

    public Guid GetDestinationNoteId()
    {
        return _destinationNoteId;
    }

    public Guid GetSourceNoteId()
    {
        return _sourceNoteId;
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