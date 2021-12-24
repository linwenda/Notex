using SmartNote.Domain.NoteComments;
using SmartNote.Domain.NoteCooperations;
using SmartNote.Domain.NoteMergeRequests;
using SmartNote.Domain.NoteMergeRequests.Exceptions;
using SmartNote.Domain.Notes.Blocks;
using SmartNote.Domain.Notes.Events;
using SmartNote.Domain.Notes.Exceptions;

namespace SmartNote.Domain.Notes
{
    public partial class Note : AggregateRoot<NoteId>
    {
        private NoteId _forkId;
        private Guid _authorId;
        private Guid _spaceId;
        private string _title;
        private bool _isDeleted;
        private NoteStatus _status;
        private NoteMemberGroup _memberGroup;
        private List<Block> _blocks;
        private List<string> _tags;

        private Note(NoteId id) : base(id)
        {
        }

        internal static Note Create(
            Guid spaceId,
            Guid userId,
            string title)
        {
            var note = new Note(new NoteId(Guid.NewGuid()));
            note.ApplyChange(new NoteCreatedEvent(
                note.Id.Value,
                spaceId,
                userId,
                DateTime.UtcNow,
                title,
                NoteStatus.Draft));

            return note;
        }

        public Note Fork(Guid userId)
        {
            CheckDeleted();
            CheckAtLeastOneRole(userId, NoteMemberRole.Author, NoteMemberRole.Writer);
            CheckNoteStatus(NoteStatus.Published, "Only published note can be forked");

            var note = new Note(new NoteId(Guid.NewGuid()));

            note.ApplyChange(new NoteForkedEvent(
                note.Id.Value,
                Id.Value,
                userId,
                _spaceId,
                DateTime.UtcNow,
                _title,
                _blocks,
                _tags));

            return note;
        }

        public void Publish(Guid userId)
        {
            CheckDeleted();
            CheckAtLeastOneRole(userId, NoteMemberRole.Author);

            if (_status != NoteStatus.Published)
            {
                ApplyChange(new NotePublishedEvent(
                    Id.Value,
                    DateTime.UtcNow,
                    NoteStatus.Published));
            }
        }

        public void Merge(
            Guid fromNoteId,
            Guid userId,
            string title,
            List<Block> blocks,
            List<string> tags)
        {
            CheckDeleted();
            CheckAtLeastOneRole(userId, NoteMemberRole.Author);

            if (_forkId == null)
            {
                throw new OnlyForkNoteCanBeMergedException();
            }

            ApplyChange(new NoteMergedEvent(
                fromNoteId,
                Id.Value,
                userId,
                title,
                blocks,
                tags));
        }

        public void Update(
            Guid userId,
            string title,
            List<Block> blocks)
        {
            CheckAtLeastOneRole(userId, NoteMemberRole.Author, NoteMemberRole.Writer);

            ApplyChange(new NoteUpdatedEvent(Id.Value, title, blocks ?? new List<Block>()));
        }

        public void Delete(Guid userId)
        {
            CheckDeleted();
            CheckAtLeastOneRole(userId, NoteMemberRole.Author);

            ApplyChange(new NoteDeletedEvent(Id.Value));
        }

        public void InviteUser(Guid userId, Guid inviteUserId, NoteMemberRole role)
        {
            CheckNoteStatus(_status, "Only published note can be invited user");
            CheckAtLeastOneRole(userId, NoteMemberRole.Author);

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
            CheckAtLeastOneRole(userId, NoteMemberRole.Author);

            if (_memberGroup.IsMember(removeUserId))
            {
                ApplyChange(new NoteMemberRemovedEvent(
                    Id.Value,
                    removeUserId,
                    DateTime.UtcNow));
            }
        }

        public NoteId GetForkId()
        {
            return _forkId;
        }

        public NoteSnapshot GetSnapshot()
        {
            Guid? formId = null;

            if (_forkId != null)
            {
                formId = _forkId.Value;
            }

            return new NoteSnapshot(Id.Value,
                Version,
                formId,
                _authorId,
                _title,
                _blocks,
                _isDeleted,
                _status,
                _memberGroup.GetMemberListSnapshot());
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
                Id.Value,
                userId,
                comment);
        }

        public NoteComment AddComment(Guid userId, string comment)
        {
            CheckNoteStatus(NoteStatus.Published);

            return NoteComment.Create(Id.Value, userId, comment);
        }

        public NoteMergeRequest CreateNoteMergeRequest(Guid userId, string title, string description)
        {
            CheckDeleted();
            CheckNoteStatus(NoteStatus.Published);

            if (_authorId != userId)
            {
                throw new NotAuthorOfTheNoteException();
            }

            if (_forkId == null)
            {
                throw new InvalidNoteMergeRequestException();
            }

            return new NoteMergeRequest(Id.Value, title, description);
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