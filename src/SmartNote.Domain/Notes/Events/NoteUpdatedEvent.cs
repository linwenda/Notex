﻿using SmartNote.Domain.Notes.Blocks;

namespace SmartNote.Domain.Notes.Events
{
    public class NoteUpdatedEvent : DomainEventBase
    {
        public Guid NoteId { get; }
        public List<Block> Blocks { get; }

        public NoteUpdatedEvent(
            Guid noteId,
            List<Block> blocks)
        {
            NoteId = noteId;
            Blocks = blocks;
        }
    }
}