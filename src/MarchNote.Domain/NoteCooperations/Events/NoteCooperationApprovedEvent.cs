using System;
using MarchNote.Domain.Shared;

namespace MarchNote.Domain.NoteCooperations.Events
{
    public class NoteCooperationApprovedEvent : DomainEventBase
    {
        public Guid SubmitterId { get; }
        public DateTime AuditTime { get; }

        public NoteCooperationApprovedEvent(Guid submitterId, DateTime auditTime)
        {
            SubmitterId = submitterId;
            AuditTime = auditTime;
        }
    }
}