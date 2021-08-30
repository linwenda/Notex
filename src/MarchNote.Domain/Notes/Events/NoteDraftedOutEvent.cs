using System;
using System.Collections.Generic;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.Notes.Events
{
    public class NoteDraftedOutEvent : DomainEventBase
    {
        public Guid NoteId { get; }
        public Guid FromNoteId { get; }
        public Guid AuthorId { get; }
        public Guid SpaceId { get; }
        public DateTime CreatedAt { get; }
        public string Title { get; }
        public string Content { get; }
        public List<string> Tags { get; }

        public NoteDraftedOutEvent(
            Guid noteId,
            Guid fromNoteId,
            Guid authorId,
            Guid spaceId,
            DateTime createdAt,
            string title,
            string content,
            List<string> tags)
        {
            NoteId = noteId;
            FromNoteId = fromNoteId;
            AuthorId = authorId;
            SpaceId = spaceId;
            CreatedAt = createdAt;
            Title = title;
            Content = content;
            Tags = tags;
        }
    }
}