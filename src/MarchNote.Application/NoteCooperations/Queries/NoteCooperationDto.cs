using System;
using MarchNote.Domain.NoteCooperations;

namespace MarchNote.Application.NoteCooperations.Queries
{
    public class NoteCooperationDto
    {
        public Guid Id { get; set; }
        public Guid NoteId { get; set; }
        public Guid SubmitterId { get; set; }
        public DateTime SubmittedAt { get; set; }
        public DateTime? AuditedAt { get; set; }
        public string Comment { get; set; }
        public string RejectReason { get; set; }
        public NoteCooperationStatus Status { get; set; }
    }
}