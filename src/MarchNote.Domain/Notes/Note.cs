using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarchNote.Domain.NoteComments;
using MarchNote.Domain.NoteCooperations;
using MarchNote.Domain.Notes.Events;
using MarchNote.Domain.Notes.Exceptions;
using MarchNote.Domain.Shared.EventSourcing;

namespace MarchNote.Domain.Notes
{
    public partial class Note : EventSourcedEntity<NoteId>
    {
        private NoteId _forkId;
        private Guid _authorId;
        private Guid _spaceId;
        private string _title;
        private string _content;
        private bool _isDeleted;
        private NoteStatus _status;
        private NoteMemberGroup _memberGroup;
        private List<string> _tags;

        private Note(NoteId id) : base(id)
        {
        }

        internal static Note Create(
            Guid spaceId,
            Guid userId,
            string title,
            string content,
            List<string> tags)
        {
            var note = new Note(new NoteId(Guid.NewGuid()));
            note.ApplyChange(new NoteCreatedEvent(
                note.Id.Value,
                spaceId,
                userId,
                DateTime.UtcNow,
                title,
                content,
                NoteStatus.Draft,
                tags ?? new List<string>()));

            return note;
        }

        public Note Fork(Guid userId)
        {
            CheckDeleted();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner, NoteMemberRole.Writer);
            CheckNoteStatus(NoteStatus.Published, "Only published note can be forked");

            var note = new Note(new NoteId(Guid.NewGuid()));

            note.ApplyChange(new NoteDraftedOutEvent(
                note.Id.Value,
                Id.Value,
                userId,
                _spaceId,
                DateTime.UtcNow,
                _title,
                _content,
                _tags));

            return note;
        }

        public void Edit(
            Guid userId,
            string title,
            string content)
        {
            CheckDeleted();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner);

            ApplyChange(new NoteEditedEvent(
                Id.Value,
                title,
                content,
                _status));
        }

        public void Publish(Guid userId)
        {
            CheckDeleted();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner);

            if (_status != NoteStatus.Published)
            {
                ApplyChange(new NotePublishedEvent(
                    Id.Value,
                    DateTime.UtcNow,
                    NoteStatus.Published));
            }
        }

        public void Merge(Guid userId)
        {
            CheckDeleted();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner);

            if (_forkId == null)
            {
                throw new OnlyForkNoteCanBeMergedException();
            }

            ApplyChange(new NoteMergedEvent(
                Id.Value,
                _forkId.Value,
                userId,
                _title,
                _content,
                _tags));
        }

        public void Update(Guid userId, string title, string content, List<string> tags)
        {
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner, NoteMemberRole.Writer);

            if (_status == NoteStatus.Published)
            {
                //New versioning
                ApplyChange(new NoteUpdatedEvent(Id.Value, title, content, tags));
            }
        }

        public void Delete(Guid userId)
        {
            CheckDeleted();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner);

            ApplyChange(new NoteDeletedEvent(Id.Value));
        }

        public void InviteUser(Guid userId, Guid inviteUserId, NoteMemberRole role)
        {
            CheckNoteStatus(_status, "Only published note can be invited user");
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner);

            if (_memberGroup.IsMember(inviteUserId))
            {
                throw new UserHasBeenJoinedThisNoteCooperationException();
            }

            ApplyChange(new NoteMemberInvitedEvent(
                Id.Value,
                inviteUserId,
                role.Value,
                DateTime.UtcNow));
        }

        public void RemoveMember(Guid userId, Guid removeUserId)
        {
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner);

            if (_memberGroup.IsMember(removeUserId))
            {
                ApplyChange(new NoteMemberRemovedEvent(
                    Id.Value,
                    removeUserId,
                    DateTime.UtcNow));
            }
        }

        public async Task<NoteCooperation> ApplyForWriterAsync(
            INoteCooperationCounter cooperationCounter,
            Guid userId,
            string comment)
        {
            CheckDeleted();
            CheckNoteStatus(NoteStatus.Published, "Only published note can be cooperated");

            if (_memberGroup.IsWriter(userId))
            {
                throw new UserHasBeenJoinedThisNoteCooperationException();
            }

            return await NoteCooperation.ApplyAsync(
                cooperationCounter,
                Id,
                userId,
                comment);
        }

        public NoteComment AddComment(Guid userId, string comment)
        {
            CheckNoteStatus(NoteStatus.Published);

            return NoteComment.Create(Id, userId, comment);
        }

        private void CheckDeleted()
        {
            if (_isDeleted)
            {
                throw new NoteHasBeenDeletedException();
            }
        }

        private void CheckAtLeastOneRole(Guid userId, params NoteMemberRole[] roles)
        {
            if (!roles.Any(r => _memberGroup.InRole(userId, r)))
            {
                throw new NotePermissionDeniedException();
            }
        }

        private void CheckNoteStatus(NoteStatus status, string errorMessage = "Invalid note status")
        {
            if (_status != status)
            {
                throw new InvalidNoteStatusException(errorMessage);
            }
        }
    }
}