using System;
using System.Collections.Generic;
using System.Linq;
using MarchNote.Domain.NoteAggregate.Events;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.SeedWork.Aggregates;
using MarchNote.Domain.Users;

namespace MarchNote.Domain.NoteAggregate
{
    public partial class Note
    {
        protected override void Apply(IDomainEvent @event)
        {
            this.When((dynamic) @event);
        }

        protected override void LoadSnapshot(ISnapshot snapshot)
        {
            var noteSnapshot = snapshot as NoteSnapshot ?? throw new AggregateRootException("Invalid snapshot");

            _authorId = new UserId(noteSnapshot.AuthorId);
            _fromId = noteSnapshot.FromId.HasValue ? new NoteId(noteSnapshot.FromId.Value) : null;
            _isDeleted = noteSnapshot.IsDeleted;
            _title = noteSnapshot.Title;
            _content = noteSnapshot.Content;
            _status = noteSnapshot.Status;
            _memberList = new NoteMemberList(noteSnapshot.MemberList.Select(m => m.ToMember()).ToList());
        }

        protected override ISnapshot CreateSnapshot()
        {
            Guid? formId = null;

            if (_fromId != null)
            {
                formId = _fromId.Value;
            }

            return new NoteSnapshot(Id.Value,
                Version,
                formId,
                _authorId.Value,
                _title,
                _content,
                _isDeleted,
                _status,
                _memberList.GetMemberListSnapshot());
        }

        private void When(NoteCreatedEvent @event)
        {
            _authorId = new UserId(@event.AuthorId);
            _title = @event.Title;
            _content = @event.Content;
            _isDeleted = false;
            _status = NoteStatus.Draft;

            _memberList = new NoteMemberList(new List<NoteMember>());
            _memberList.AddMember(_authorId, NoteMemberRole.Owner);
        }

        private void When(NoteEditedEvent @event)
        {
            _title = @event.Title;
            _content = @event.Content;
        }

        private void When(NoteDraftedOutEvent @event)
        {
            _authorId = new UserId(@event.AuthorId);
            _fromId = new NoteId(@event.FromNoteId);
            _title = @event.Title;
            _content = @event.Content;
            _isDeleted = false;
            _status = NoteStatus.Draft;

            _memberList = new NoteMemberList(new List<NoteMember>());
            _memberList.AddMember(_authorId, NoteMemberRole.Owner);
        }

        private void When(NoteDeletedEvent @event)
        {
            _isDeleted = true;
        }

        private void When(NotePublishedEvent @event)
        {
            _status = NoteStatus.Published;
        }

        private void When(NoteMergedEvent @event)
        {
            _isDeleted = true;
        }

        private void When(NoteUpdatedEvent @event)
        {
            _title = @event.Title;
            _content = @event.Content;
        }

        private void When(NoteMemberInvitedEvent @event)
        {
            _memberList.AddMember(new UserId(@event.MemberId), NoteMemberRole.Of(@event.Role));
        }

        private void When(NoteMemberRemovedEvent @event)
        {
            _memberList.RemoveMember(new UserId(@event.MemberId));
        }
    }
}