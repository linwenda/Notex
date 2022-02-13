namespace SmartNote.Core.Domain.Notes.Events
{
    public class NoteCreatedEvent : DomainEventBase
    {
        public DateTimeOffset CreationTime { get; }
        public Guid NoteId { get; }
        public Guid SpaceId { get; }
        public Guid AuthorId { get; }
        public string Title { get; }
        public NoteStatus Status { get; }

        public NoteCreatedEvent(
            Guid noteId,
            Guid spaceId,
            Guid authorId,
            DateTime creationTime,
            string title,
            NoteStatus status)
        {
            NoteId = noteId;
            SpaceId = spaceId;
            AuthorId = authorId;
            CreationTime = creationTime;
            Title = title;
            Status = status;
        }
    }
}