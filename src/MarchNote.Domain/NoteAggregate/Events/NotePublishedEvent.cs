﻿using System;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.NoteAggregate.Events
{
    public class NotePublishedEvent : DomainEventBase
    {
        public Guid NoteId { get; }
        public DateTime PublishedAt { get; }
        public NoteStatus Status { get; }

        public NotePublishedEvent(Guid noteId, DateTime publishedAt, NoteStatus status)
        {
            NoteId = noteId;
            PublishedAt = publishedAt;
            Status = status;
        }
    }
}