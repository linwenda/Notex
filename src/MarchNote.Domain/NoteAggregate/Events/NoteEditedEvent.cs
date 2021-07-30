using System;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.NoteAggregate.Events
{
    public class NoteEditedEvent : DomainEventBase
    {
        public Guid NoteId { get; }
        public string Title { get; }
        public string Content { get; }
        public NoteStatus Status { get; }

        public NoteEditedEvent(
            Guid noteId,
            string title,
            string content,
            NoteStatus status)
        {
            NoteId = noteId;
            Title = title;
            Content = content;
            Status = status;
        }
    }
}