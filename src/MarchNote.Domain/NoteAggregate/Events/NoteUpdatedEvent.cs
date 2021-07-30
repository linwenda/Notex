using System;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.NoteAggregate.Events
{
    public class NoteUpdatedEvent : DomainEventBase
    {
        public Guid NoteId { get; }
        public string Title { get; }
        public string Content { get; }

        public NoteUpdatedEvent(Guid noteId, string title, string content)
        {
            NoteId = noteId;
            Title = title;
            Content = content;
        }
    }
}