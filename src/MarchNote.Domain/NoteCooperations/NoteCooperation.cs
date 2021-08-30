using System;
using MarchNote.Domain.NoteCooperations.Events;
using MarchNote.Domain.Notes;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Users;

namespace MarchNote.Domain.NoteCooperations
{
    public class NoteCooperation : Entity
    {
        public NoteCooperationId Id { get; private set; }
        public NoteId NoteId { get; private set; }
        public UserId SubmitterId { get; private set; }
        public DateTime SubmittedAt { get; private set; }
        public UserId AuditorId { get; private set; }
        public DateTime? AuditedAt { get; private set; }
        public NoteCooperationStatus Status { get; private set; }
        public string Comment { get; private set; }
        public string RejectReason { get; private set; }

        private NoteCooperation()
        {
        }

        public NoteCooperation(NoteId noteId, UserId userId, string comment)
        {
            Id = new NoteCooperationId(Guid.NewGuid());
            NoteId = noteId;
            SubmitterId = userId;
            Comment = comment;
            SubmittedAt = DateTime.UtcNow;
            Status = NoteCooperationStatus.Pending;
        }

        public static NoteCooperation Apply(
            INoteCooperationCounter cooperationCounter,
            NoteId noteId,
            UserId userId,
            string comment)
        {
            if (cooperationCounter.CountPending(userId, noteId) > 0)
            {
                throw new BusinessException(ExceptionCode.CooperationApplicationInProgress,
                    "Application in progress");
            }

            return new NoteCooperation(noteId, userId, comment);
        }

        public void Approve(UserId userId, NoteMemberGroup memberList)
        {
            if (Status != NoteCooperationStatus.Pending)
            {
                throw new BusinessException(ExceptionCode.CooperationOnlyPendingCanBeApproved,
                    "Only pending status can be approved");
            }
            
            CheckNoteOwner(userId, memberList);

            Status = NoteCooperationStatus.Approved;
            AuditorId = userId;
            AuditedAt = DateTime.UtcNow;

            AddDomainEvent(new NoteCooperationApprovedEvent(userId.Value, AuditedAt.Value));
        }

        public void Reject(UserId userId, NoteMemberGroup memberList, string rejectReason)
        {
            if (Status != NoteCooperationStatus.Pending)
            {
                throw new BusinessException(ExceptionCode.CooperationOnlyPendingCanBeRejected,
                    "Only pending status can be rejected");
            }
            
            CheckNoteOwner(userId, memberList);

            Status = NoteCooperationStatus.Rejected;
            AuditorId = userId;
            AuditedAt = DateTime.UtcNow;
            RejectReason = rejectReason;

            AddDomainEvent(new NoteCooperationRejectedEvent(userId.Value, AuditedAt.Value, RejectReason));
        }

        private void CheckNoteOwner(UserId userId, NoteMemberGroup memberList)
        {
            if (!memberList.IsOwner(userId))
            {
                throw new BusinessException(ExceptionCode.CooperationOnlyNoteOwnerCanBeApproved,
                    "Only note owner can be approved");
            }
        }
    }
}