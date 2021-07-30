using System;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.NoteAggregate.Events
{
    public class NoteDraftedOutEvent : DomainEventBase
    {
        public Guid NoteId { get; }
        public Guid FromNoteId { get; }
        public Guid AuthorId { get; }
        public DateTime CreatedAt { get; }
        public string Title { get; }
        public string Content { get; }

        public NoteDraftedOutEvent(
            Guid noteId,
            Guid fromNoteId,
            Guid authorId,
            DateTime createdAt,
            string title,
            string content)
        {
            NoteId = noteId;
            FromNoteId = fromNoteId;
            AuthorId = authorId;
            CreatedAt = createdAt;
            Title = title;
            Content = content;
        }
    }
}