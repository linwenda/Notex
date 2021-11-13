using System;
using MarchNote.Domain.Shared;

namespace MarchNote.Domain.NoteMergeRequests.Events
{
    public class NoteMergeRequestMergedEvent : DomainEventBase
    {
        public Guid NoteMergeRequestId { get; }
        public Guid NoteId { get; }

        public NoteMergeRequestMergedEvent(Guid noteMergeRequestId, Guid noteId)
        {
            NoteMergeRequestId = noteMergeRequestId;
            NoteId = noteId;
        }
    }
}