using System;
using System.Collections.Generic;
using MarchNote.Domain.Shared;

namespace MarchNote.Domain.Notes.Events
{
    public class NoteCreatedEvent : DomainEventBase
    {
        public Guid NoteId { get; }
        public Guid SpaceId { get; }
        public Guid AuthorId { get; }
        public DateTime CreatedAt { get; }
        public string Title { get; }
        public string Content { get; }
        public NoteStatus Status { get; }
        public List<string> Tags { get; }

        public NoteCreatedEvent(
            Guid noteId,
            Guid spaceId,
            Guid authorId,
            DateTime createdAt,
            string title,
            string content, 
            NoteStatus status,
            List<string> tags)
        {
            NoteId = noteId;
            SpaceId = spaceId;
            AuthorId = authorId;
            CreatedAt = createdAt;
            Title = title;
            Content = content;
            Status = status;
            Tags = tags;
        }
    }
}