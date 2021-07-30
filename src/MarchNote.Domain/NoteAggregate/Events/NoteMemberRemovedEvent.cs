﻿using System;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.NoteAggregate.Events
{
    public class NoteMemberRemovedEvent : DomainEventBase
    {
        public Guid NoteId { get; }
        public Guid MemberId { get; }
        public DateTime LeaveAt { get; }

        public NoteMemberRemovedEvent(
            Guid noteId, 
            Guid memberId,
            DateTime leaveAt)
        {
            NoteId = noteId;
            MemberId = memberId;
            LeaveAt = leaveAt;
        }
    }
}