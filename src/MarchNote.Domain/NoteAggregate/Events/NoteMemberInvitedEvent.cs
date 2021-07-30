﻿using System;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.NoteAggregate.Events
{
    public class NoteMemberInvitedEvent : DomainEventBase
    {
        public Guid NoteId { get; }
        public Guid MemberId { get; }
        public string Role { get; }
        public DateTime JoinedAt { get; }

        public NoteMemberInvitedEvent(
            Guid noteId,
            Guid memberId,
            string role,
            DateTime joinedAt)
        {
            NoteId = noteId;
            MemberId = memberId;
            Role = role;
            JoinedAt = joinedAt;
        }
    }
}