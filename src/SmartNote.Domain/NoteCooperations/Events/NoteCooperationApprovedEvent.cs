namespace SmartNote.Domain.NoteCooperations.Events
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