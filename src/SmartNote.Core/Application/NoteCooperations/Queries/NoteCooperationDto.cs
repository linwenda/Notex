using SmartNote.Core.Domain.NoteCooperations;

namespace SmartNote.Core.Application.NoteCooperations.Queries
{
    public class NoteCooperationDto
    {
        public Guid Id { get; set; }
        public Guid NoteId { get; set; }
        public Guid SubmitterId { get; set; }
        public DateTime SubmittedAt { get; set; }
        public DateTime? AuditTime { get; set; }
        public string Comment { get; set; }
        public string RejectReason { get; set; }
        public NoteCooperationStatus Status { get; set; }
    }
}