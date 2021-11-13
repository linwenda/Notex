using System;
using MarchNote.Domain.Shared;

namespace MarchNote.Domain.NoteMergeRequests.Events
{
    public class NoteMergeRequestClosedEvent : DomainEventBase
    {
        public Guid NoteMergeRequestId { get; }

        public NoteMergeRequestClosedEvent(Guid NoteMergeRequestId)
        {
            this.NoteMergeRequestId = NoteMergeRequestId;
        }
    }
}