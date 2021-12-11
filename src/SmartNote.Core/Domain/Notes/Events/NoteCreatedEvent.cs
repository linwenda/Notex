﻿namespace SmartNote.Core.Domain.Notes.Events
{
    public class NoteCreatedEvent : DomainEventBase
    {
        public DateTimeOffset CreationTime { get; }
        public Guid NoteId { get; }
        public Guid SpaceId { get; }
        public Guid AuthorId { get; }
        public string Title { get; }
        public string Content { get; }
        public NoteStatus Status { get; }
        public List<string> Tags { get; }

        public NoteCreatedEvent(
            Guid noteId,
            Guid spaceId,
            Guid authorId,
            DateTime creationTime,
            string title,
            string content,
            NoteStatus status,
            List<string> tags)
        {
            NoteId = noteId;
            SpaceId = spaceId;
            AuthorId = authorId;
            CreationTime = creationTime;
            Title = title;
            Content = content;
            Status = status;
            Tags = tags;
        }
    }
}