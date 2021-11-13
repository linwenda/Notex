using System;
using MarchNote.Domain.Shared;

namespace MarchNote.Domain.NoteCooperations.Events
{
    public class NoteCooperationRejectedEvent : DomainEventBase
    {
        public Guid SubmitterId { get; }
        public DateTime AuditTime { get; }
        public string RejectReason { get; }

        public NoteCooperationRejectedEvent(
            Guid submitterId,
            DateTime auditTime,
            string rejectReason)
        {
            SubmitterId = submitterId;
            AuditTime = auditTime;
            RejectReason = rejectReason;
        }
    }
}