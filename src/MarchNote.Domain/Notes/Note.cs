using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarchNote.Domain.NoteComments;
using MarchNote.Domain.NoteCooperations;
using MarchNote.Domain.Notes.Events;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.SeedWork.EventSourcing;
using MarchNote.Domain.Spaces;

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

        public Note DraftOut(Guid userId)
        {
            CheckPublished();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner, NoteMemberRole.Writer);

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
            CheckDelete();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner);

            ApplyChange(new NoteEditedEvent(
                Id.Value,
                title,
                content,
                _status));
        }

        public void Publish(Guid userId)
        {
            CheckDelete();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner);

            if (_forkId != null)
            {
                throw new NoteException("Only main note can be published");
            }

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
            CheckDelete();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner);

            if (_forkId == null)
            {
                throw new NoteException("Only draft out note can be merged");
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
            CheckPublished();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner, NoteMemberRole.Writer);

            ApplyChange(new NoteUpdatedEvent(Id.Value, title, content, tags));
        }

        public void Delete(Guid userId)
        {
            CheckDelete();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner);

            ApplyChange(new NoteDeletedEvent(Id.Value));
        }

        public void InviteUser(Guid userId, Guid inviteUserId, NoteMemberRole role)
        {
            CheckPublished();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner);

            if (_memberGroup.IsMember(inviteUserId))
            {
                throw new NoteException("User has been joined");
            }

            ApplyChange(new NoteMemberInvitedEvent(
                Id.Value,
                inviteUserId,
                role.Value,
                DateTime.UtcNow));
        }

        public void RemoveMember(Guid userId, Guid removeUserId)
        {
            CheckPublished();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner);

            if (!_memberGroup.IsMember(removeUserId))
            {
                throw new NoteException("Member has been removed");
            }

            ApplyChange(new NoteMemberRemovedEvent(
                Id.Value,
                removeUserId,
                DateTime.UtcNow));
        }

        public async Task<NoteCooperation> ApplyForWriterAsync(
            INoteCooperationCounter cooperationCounter,
            Guid userId,
            string comment)
        {
            CheckPublished();

            if (_memberGroup.IsWriter(userId))
            {
                throw new NoteException("You already is the writer of the note");
            }

            return await NoteCooperation.ApplyAsync(
                cooperationCounter,
                Id,
                userId,
                comment);
        }

        public NoteComment AddComment(Guid userId, string comment)
        {
            CheckPublished();

            return NoteComment.Create(Id, userId, comment);
        }

        private void CheckDelete()
        {
            if (_isDeleted)
            {
                throw new NoteException("Note has been deleted");
            }
        }

        private void CheckAtLeastOneRole(Guid userId, params NoteMemberRole[] roles)
        {
            if (!roles.Any(r => _memberGroup.InRole(userId, r)))
            {
                throw new NoteException("Permission denied");
            }
        }

        private void CheckPublished()
        {
            CheckDelete();
            if (_status != NoteStatus.Published)
            {
                throw new NoteException("Only published note can be operated");
            }
        }
    }
}