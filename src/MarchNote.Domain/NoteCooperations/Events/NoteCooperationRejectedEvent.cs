using System;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.NoteCooperations.Events
{
    public class NoteCooperationRejectedEvent : DomainEventBase
    {
        public Guid SubmitterId { get; }
        public DateTime AuditedAt { get; }
        public string RejectReason { get; }

        public NoteCooperationRejectedEvent(
            Guid submitterId,
            DateTime auditedAt,
            string rejectReason)
        {
            SubmitterId = submitterId;
            AuditedAt = auditedAt;
            RejectReason = rejectReason;
        }
    }
}