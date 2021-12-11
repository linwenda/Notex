﻿namespace SmartNote.Core.Domain.Notes.Events
{
    public class NoteForkedEvent : DomainEventBase
    {
        public Guid NoteId { get; }
        public Guid FromNoteId { get; }
        public Guid AuthorId { get; }
        public Guid SpaceId { get; }
        
        public DateTimeOffset CreationTime { get; }
        public string Title { get; }
        public string Content { get; }
        public List<string> Tags { get; }

        public NoteForkedEvent(
            Guid noteId,
            Guid fromNoteId,
            Guid authorId,
            Guid spaceId,
            DateTimeOffset creationTime,
            string title,
            string content,
            List<string> tags)
        {
            NoteId = noteId;
            FromNoteId = fromNoteId;
            AuthorId = authorId;
            SpaceId = spaceId;
            CreationTime = creationTime;
            Title = title;
            Content = content;
            Tags = tags;
        }
    }
}