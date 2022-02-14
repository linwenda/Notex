using SmartNote.Core.Ddd;

namespace SmartNote.Domain.Notes.Events
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