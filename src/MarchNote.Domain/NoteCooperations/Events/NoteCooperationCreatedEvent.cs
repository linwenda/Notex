using System;
using MarchNote.Domain.Shared;

namespace MarchNote.Domain.NoteCooperations.Events
{
    public class NoteCooperationCreatedEvent : DomainEventBase
    {
        public Guid CooperationId { get; }
        public Guid NoteId { get; }
        public Guid SubmitterId { get; }
        public DateTime SubmittedAt { get; }
        public string Comment { get; }

        public NoteCooperationCreatedEvent(
            Guid cooperationId,
            Guid noteId,
            Guid submitterId,
            DateTime submittedAt,
            string comment)
        {
            CooperationId = cooperationId;
            NoteId = noteId;
            SubmitterId = submitterId;
            SubmittedAt = submittedAt;
            Comment = comment;
        }
    }
}