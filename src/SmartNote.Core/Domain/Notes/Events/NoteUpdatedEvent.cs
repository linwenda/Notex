using SmartNote.Core.Domain.Notes.Blocks;

namespace SmartNote.Core.Domain.Notes.Events
{
    public class NoteUpdatedEvent : DomainEventBase
    {
        public Guid NoteId { get; }
        public string Title { get; }
        public List<Block> Blocks { get; }

        public NoteUpdatedEvent(
            Guid noteId,
            string title,
            List<Block> blocks)
        {
            NoteId = noteId;
            Title = title;
            Blocks = blocks;
        }
    }
}