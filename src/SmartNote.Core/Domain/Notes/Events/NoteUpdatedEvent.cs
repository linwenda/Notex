﻿namespace SmartNote.Core.Domain.Notes.Events
{
    public class NoteUpdatedEvent : DomainEventBase
    {
        public Guid NoteId { get; }
        public string Title { get; }
        public string Content { get; }
        public List<string> Tags { get; }
        public NoteStatus Status { get; }

        public NoteUpdatedEvent(Guid noteId, string title, string content, List<string> tags, NoteStatus status)
        {
            NoteId = noteId;
            Title = title;
            Content = content;
            Tags = tags;
            Status = status;
        }
    }
}