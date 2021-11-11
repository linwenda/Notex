using System;
using System.Collections.Generic;
using MarchNote.Domain.Shared;

namespace MarchNote.Domain.Notes.Events
{
    public class NoteUpdatedEvent : DomainEventBase
    {
        public Guid NoteId { get; }
        public string Title { get; }
        public string Content { get; }
        public List<string> Tags { get; }

        public NoteUpdatedEvent(Guid noteId, string title, string content,List<string> tags)
        {
            NoteId = noteId;
            Title = title;
            Content = content;
            Tags = tags;
        }
    }
}