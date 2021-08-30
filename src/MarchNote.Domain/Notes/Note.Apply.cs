using System;
using System.Collections.Generic;
using System.Linq;
using MarchNote.Domain.Notes.Events;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.SeedWork.EventSourcing;
using MarchNote.Domain.Spaces;
using MarchNote.Domain.Users;

namespace MarchNote.Domain.Notes
{
    public partial class Note
    {
        protected override void Apply(IDomainEvent @event)
        {
            this.When((dynamic) @event);
        }

        protected override void LoadSnapshot(ISnapshot snapshot)
        {
            var noteSnapshot = snapshot as NoteSnapshot ?? throw new EventSourcedException("Invalid snapshot");

            _authorId = new UserId(noteSnapshot.AuthorId);
            _fromId = noteSnapshot.FromId.HasValue ? new NoteId(noteSnapshot.FromId.Value) : null;
            _isDeleted = noteSnapshot.IsDeleted;
            _title = noteSnapshot.Title;
            _content = noteSnapshot.Content;
            _status = noteSnapshot.Status;
            _memberGroup = new NoteMemberGroup(noteSnapshot.MemberList.Select(m => m.ToMember()).ToList());
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
                _memberGroup.GetMemberListSnapshot());
        }

        private void When(NoteCreatedEvent @event)
        {
            _authorId = new UserId(@event.AuthorId);
            _spaceId = new SpaceId(@event.SpaceId);
            _title = @event.Title;
            _content = @event.Content;
            _isDeleted = false;
            _status = NoteStatus.Draft;
            _tags = @event.Tags;

            _memberGroup = new NoteMemberGroup(new List<NoteMember>());
            _memberGroup.AddMember(_authorId, NoteMemberRole.Owner);
        }

        private void When(NoteEditedEvent @event)
        {
            _title = @event.Title;
            _content = @event.Content;
        }

        private void When(NoteDraftedOutEvent @event)
        {
            _authorId = new UserId(@event.AuthorId);
            _spaceId = new SpaceId(@event.SpaceId);
            _fromId = new NoteId(@event.FromNoteId);
            _title = @event.Title;
            _content = @event.Content;
            _isDeleted = false;
            _status = NoteStatus.Draft;
            _tags = @event.Tags;

            _memberGroup = new NoteMemberGroup(new List<NoteMember>());
            _memberGroup.AddMember(_authorId, NoteMemberRole.Owner);
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
            _memberGroup.AddMember(new UserId(@event.MemberId), NoteMemberRole.Of(@event.Role));
        }

        private void When(NoteMemberRemovedEvent @event)
        {
            _memberGroup.RemoveMember(new UserId(@event.MemberId));
        }
    }
}