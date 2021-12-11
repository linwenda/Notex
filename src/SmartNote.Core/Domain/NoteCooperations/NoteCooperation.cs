using SmartNote.Core.Domain.NoteCooperations.Events;
using SmartNote.Core.Domain.NoteCooperations.Exceptions;
using SmartNote.Core.Domain.Notes;
using SmartNote.Core.Domain.Notes.Exceptions;

namespace SmartNote.Core.Domain.NoteCooperations
{
    public sealed class NoteCooperation : Entity<Guid>, IHasCreationTime
    {
        public DateTimeOffset CreationTime { get; set; }
        public Guid NoteId { get; private set; }
        public Guid SubmitterId { get; private set; }
        public Guid? AuditorId { get; private set; }
        public DateTime? AuditTime { get; private set; }
        public NoteCooperationStatus Status { get; private set; }
        public string Comment { get; private set; }
        public string RejectReason { get; private set; }

        private NoteCooperation()
        {
        }

        private NoteCooperation(Guid noteId, Guid userId, string comment)
        {
            Id = Guid.NewGuid();
            NoteId = noteId;
            SubmitterId = userId;
            Comment = comment;
            CreationTime = DateTime.UtcNow;
            Status = NoteCooperationStatus.Pending;
        }

        public static async Task<NoteCooperation> ApplyAsync(
            INoteCooperationCounter cooperationCounter,
            Guid noteId,
            Guid userId,
            string comment)
        {
            if (await cooperationCounter.CountPendingAsync(userId, noteId) > 0)
            {
                throw new ApplicationInProgressException();
            }

            return new NoteCooperation(noteId, userId, comment);
        }

        public async Task ApproveAsync(INoteChecker noteManager, Guid userId)
        {
            if (Status != NoteCooperationStatus.Pending)
            {
                throw new InvalidCooperationStatusException();
            }

            await CheckNoteAuthorAsync(noteManager, userId);

            Status = NoteCooperationStatus.Approved;
            AuditorId = userId;
            AuditTime = DateTime.UtcNow;

            AddDomainEvent(new NoteCooperationApprovedEvent(userId, AuditTime.Value));
        }

        public async Task RejectAsync(INoteChecker noteManager, Guid userId, string rejectReason)
        {
            if (Status != NoteCooperationStatus.Pending)
            {
                throw new InvalidCooperationStatusException();
            }

            await CheckNoteAuthorAsync(noteManager, userId);

            Status = NoteCooperationStatus.Rejected;
            AuditorId = userId;
            AuditTime = DateTime.UtcNow;
            RejectReason = rejectReason;

            AddDomainEvent(new NoteCooperationRejectedEvent(userId, AuditTime.Value, RejectReason));
        }

        private async Task CheckNoteAuthorAsync(INoteChecker noteManager, Guid userId)
        {
            if (!await noteManager.IsAuthorAsync(NoteId, userId))
            {
                throw new NotAuthorOfTheNoteException();
            }
        }
    }
}