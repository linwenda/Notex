using System;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.Notes.Events
{
    public class NoteDeletedEvent : DomainEventBase
    {
        public Guid NoteId { get; }

        public NoteDeletedEvent(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}