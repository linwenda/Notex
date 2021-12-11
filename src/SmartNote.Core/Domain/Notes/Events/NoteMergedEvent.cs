namespace SmartNote.Core.Domain.Notes.Events
{
    public class NoteMergedEvent : DomainEventBase
    {
        public Guid FromNoteId { get; }
        public Guid NoteId { get; }
        public Guid AuthorId { get; }
        public string Title { get; }
        public string Content { get; }
        public List<string> Tags { get; }

        public NoteMergedEvent(
            Guid fromNoteId,
            Guid noteId,
            Guid authorId,
            string title,
            string content,
            List<string> tags)
        {
            FromNoteId = fromNoteId;
            NoteId = noteId;
            AuthorId = authorId;
            Title = title;
            Content = content;
            Tags = tags;
        }
    }
}