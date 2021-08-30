using System;
using System.Collections.Generic;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.Notes.Events
{
    public class NoteMergedEvent : DomainEventBase
    {
        public Guid NoteId { get; }
        public Guid FromNoteId { get; }
        public Guid AuthorId { get; }
        public string Title { get; }
        public string Content { get; }
        public List<string> Tags { get; }

        public NoteMergedEvent(
            Guid noteId,
            Guid fromNoteId,
            Guid authorId,
            string title,
            string content,
            List<string> tags)
        {
            NoteId = noteId;
            FromNoteId = fromNoteId;
            AuthorId = authorId;
            Title = title;
            Content = content;
            Tags = tags;
        }
    }
}