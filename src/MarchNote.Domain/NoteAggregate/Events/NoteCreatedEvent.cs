using System;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.NoteAggregate.Events
{
    public class NoteCreatedEvent : DomainEventBase
    {
        public Guid NoteId { get; }
        public Guid AuthorId { get; }
        public DateTime CreatedAt { get; }
        public string Title { get; }
        public string Content { get; }
        public NoteStatus Status { get; }

        public NoteCreatedEvent(
            Guid noteId,
            Guid authorId,
            DateTime createdAt,
            string title,
            string content, 
            NoteStatus status)
        {
            NoteId = noteId;
            AuthorId = authorId;
            CreatedAt = createdAt;
            Title = title;
            Content = content;
            Status = status;
        }
    }
}