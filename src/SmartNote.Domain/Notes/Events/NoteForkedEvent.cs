using SmartNote.Domain.Notes.Blocks;

namespace SmartNote.Domain.Notes.Events
{
    public class NoteForkedEvent : DomainEventBase
    {
        public Guid NoteId { get; }
        public Guid FromNoteId { get; }
        public Guid AuthorId { get; }
        public Guid SpaceId { get; }
        public DateTimeOffset CreationTime { get; }
        public string Title { get; }
        public List<Block> Blocks { get; }
        public List<string> Tags { get; }

        public NoteForkedEvent(
            Guid noteId,
            Guid fromNoteId,
            Guid authorId,
            Guid spaceId,
            DateTimeOffset creationTime,
            string title,
            List<Block> blocks,
            List<string> tags)
        {
            NoteId = noteId;
            FromNoteId = fromNoteId;
            AuthorId = authorId;
            SpaceId = spaceId;
            CreationTime = creationTime;
            Title = title;
            Blocks = blocks;
            Tags = tags;
        }
    }
}