using System;
using System.Threading.Tasks;
using MarchNote.Domain.NoteCooperations.Events;
using MarchNote.Domain.NoteCooperations.Exceptions;
using MarchNote.Domain.Notes;
using MarchNote.Domain.Notes.Exceptions;
using MarchNote.Domain.Shared;
using MarchNote.Domain.Users;

namespace MarchNote.Domain.NoteCooperations
{
    public sealed class NoteCooperation : Entity<Guid>
    {
        public NoteId NoteId { get; private set; }
        public Guid SubmitterId { get; private set; }
        public DateTime SubmittedAt { get; private set; }
        public Guid? AuditorId { get; private set; }
        public DateTime? AuditedAt { get; private set; }
        public NoteCooperationStatus Status { get; private set; }
        public string Comment { get; private set; }
        public string RejectReason { get; private set; }

        private NoteCooperation()
        {
        }

        private NoteCooperation(NoteId noteId, Guid userId, string comment)
        {
            Id = Guid.NewGuid();
            NoteId = noteId;
            SubmitterId = userId;
            Comment = comment;
            SubmittedAt = DateTime.UtcNow;
            Status = NoteCooperationStatus.Pending;
        }

        public static async Task<NoteCooperation> ApplyAsync(
            INoteCooperationCounter cooperationCounter,
            NoteId noteId,
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
            AuditedAt = DateTime.UtcNow;

            AddDomainEvent(new NoteCooperationApprovedEvent(userId, AuditedAt.Value));
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
            AuditedAt = DateTime.UtcNow;
            RejectReason = rejectReason;

            AddDomainEvent(new NoteCooperationRejectedEvent(userId, AuditedAt.Value, RejectReason));
        }

        private async Task CheckNoteAuthorAsync(INoteChecker noteManager, Guid userId)
        {
            if (!await noteManager.IsAuthorAsync(NoteId.Value, userId))
            {
                throw new NotAuthorOfTheNoteException();
            }
        }
    }
}