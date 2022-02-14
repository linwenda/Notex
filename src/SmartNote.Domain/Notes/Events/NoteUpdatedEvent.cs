using SmartNote.Core.Ddd;
using SmartNote.Domain.Notes.Blocks;

namespace SmartNote.Domain.Notes.Events
{
    public class NoteUpdatedEvent : DomainEventBase
    {
        public Guid NoteId { get; }
        public List<Block> Content { get; }

        public NoteUpdatedEvent(
            Guid noteId,
            List<Block> content)
        {
            NoteId = noteId;
            Content = content;
        }
    }
}