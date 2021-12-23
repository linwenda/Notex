using SmartNote.Core.Domain.Notes.Blocks;

namespace SmartNote.Core.Domain.Notes.Events
{
    public class NoteMergedEvent : DomainEventBase
    {
        public Guid FromNoteId { get; }
        public Guid NoteId { get; }
        public Guid AuthorId { get; }
        public string Title { get; }
        public List<Block> Blocks { get; }
        public List<string> Tags { get; }

        public NoteMergedEvent(
            Guid fromNoteId,
            Guid noteId,
            Guid authorId,
            string title,
            List<Block> blocks,
            List<string> tags)
        {
            FromNoteId = fromNoteId;
            NoteId = noteId;
            AuthorId = authorId;
            Title = title;
            Blocks = blocks;
            Tags = tags;
        }
    }
}