using System;
using System.Linq;
using MarchNote.Domain.NoteAggregate.Events;
using MarchNote.Domain.NoteComments;
using MarchNote.Domain.NoteCooperations;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.SeedWork.Aggregates;
using MarchNote.Domain.Spaces;
using MarchNote.Domain.Users;

namespace MarchNote.Domain.NoteAggregate
{
    public partial class Note : AggregateRoot<NoteId>
    {
        private NoteId _fromId;
        private UserId _authorId;
        private SpaceId _spaceId;
        private SpaceFolderId _spaceFolderId;
        private string _title;
        private string _content;
        private bool _isDeleted;
        private NoteStatus _status;
        private NoteMemberList _memberList;

        public Note(NoteId id) : base(id)
        {
        }

        public void Create(
            UserId authorId,
            string title,
            string content)
        {
            ApplyChange(new NoteCreatedEvent(
                Id.Value,
                authorId.Value,
                DateTime.UtcNow,
                title,
                content,
                NoteStatus.Draft));
        }

        public Note DraftOut(UserId userId)
        {
            CheckPublished();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner, NoteMemberRole.Writer);

            var note = new Note(new NoteId(Guid.NewGuid()));

            note.ApplyChange(new NoteDraftedOutEvent(
                note.Id.Value,
                Id.Value,
                userId.Value,
                DateTime.UtcNow,
                _title,
                _content));

            return note;
        }

        public void Edit(
            UserId userId,
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

        public void Publish(UserId userId)
        {
            CheckDelete();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner);

            if (_fromId != null)
            {
                throw new BusinessException(ExceptionCode.NotePublishOnlyByMain,
                    "Only main note can be published");
            }

            if (_status != NoteStatus.Published)
            {
                ApplyChange(new NotePublishedEvent(
                    Id.Value,
                    DateTime.UtcNow,
                    NoteStatus.Published));
            }
        }

        public void Merge(UserId userId)
        {
            CheckDelete();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner);

            if (_fromId == null)
            {
                throw new BusinessException(ExceptionCode.NoteMergeOnlyByDraftOutNote,
                    "Only draft out note can be merged");
            }

            ApplyChange(new NoteMergedEvent(
                Id.Value,
                _fromId.Value,
                userId.Value,
                _title,
                _content));
        }

        public void Update(UserId userId, string title, string content)
        {
            CheckPublished();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner, NoteMemberRole.Writer);

            ApplyChange(new NoteUpdatedEvent(Id.Value, title, content));
        }

        public void Delete(UserId userId)
        {
            CheckDelete();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner);

            ApplyChange(new NoteDeletedEvent(Id.Value));
        }

        public void InviteUser(UserId userId, UserId inviteUserId, NoteMemberRole role)
        {
            CheckPublished();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner);

            if (_memberList.IsMember(inviteUserId))
            {
                throw new BusinessException(ExceptionCode.NoteUserHasBeenJoined,
                    "User has been joined");
            }

            ApplyChange(new NoteMemberInvitedEvent(
                Id.Value,
                inviteUserId.Value,
                role.Value,
                DateTime.UtcNow));
        }

        public void RemoveMember(UserId userId, UserId removeUserId)
        {
            CheckPublished();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner);

            if (!_memberList.IsMember(removeUserId))
            {
                throw new BusinessException(ExceptionCode.NoteMemberHasBeenRemoved,
                    "Member has been removed");
            }

            ApplyChange(new NoteMemberRemovedEvent(
                Id.Value,
                removeUserId.Value,
                DateTime.UtcNow));
        }

        public NoteCooperation ApplyForWriter(INoteCooperationCounter cooperationCounter, UserId userId, string comment)
        {
            CheckPublished();

            if (_memberList.IsWriter(userId))
            {
                throw new BusinessException(ExceptionCode.NoteCooperationWriterExists,
                    "You already is the writer of the note");
            }

            return NoteCooperation.Apply(
                cooperationCounter,
                Id,
                userId,
                comment);
        }

        public NoteComment AddComment(UserId userId, string comment)
        {
            CheckPublished();

            return NoteComment.Create(Id, userId, comment);
        }

        private void CheckDelete()
        {
            if (_isDeleted)
            {
                throw new BusinessException(ExceptionCode.NoteHasBeenDeleted,
                    "Note has been deleted");
            }
        }

        private void CheckAtLeastOneRole(UserId userId, params NoteMemberRole[] roles)
        {
            if (!roles.Any(r => _memberList.InRole(userId, r)))
            {
                throw new BusinessException(ExceptionCode.NotePermissionDenied,
                    "Permission denied");
            }
        }

        private void CheckPublished()
        {
            CheckDelete();
            if (_status != NoteStatus.Published)
            {
                throw new BusinessException(ExceptionCode.NoteStatusMustBePublished,
                    "Only published note can be operated");
            }
        }
    }
}