using System;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.NoteCooperations.Events
{
    public class NoteCooperationApprovedEvent : DomainEventBase
    {
        public Guid SubmitterId { get; }
        public DateTime AuditedAt { get; }

        public NoteCooperationApprovedEvent(Guid submitterId, DateTime auditedAt)
        {
            SubmitterId = submitterId;
            AuditedAt = auditedAt;
        }
    }
}