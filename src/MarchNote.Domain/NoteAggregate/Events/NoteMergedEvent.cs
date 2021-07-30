using System;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.NoteAggregate.Events
{
    public class NoteMergedEvent : DomainEventBase
    {
        public Guid NoteId { get; }
        public Guid FromNoteId { get; }
        public Guid AuthorId { get; }
        public string Title { get; }
        public string Content { get; }

        public NoteMergedEvent(
            Guid noteId,
            Guid fromNoteId,
            Guid authorId,
            string title,
            string content)
        {
            NoteId = noteId;
            FromNoteId = fromNoteId;
            AuthorId = authorId;
            Title = title;
            Content = content;
        }
    }
}