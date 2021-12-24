namespace SmartNote.Domain.NoteMergeRequests.Events
{
    public class NoteMergeRequestClosedEvent : DomainEventBase
    {
        public Guid NoteMergeRequestId { get; }

        public NoteMergeRequestClosedEvent(Guid noteMergeRequestId)
        {
            NoteMergeRequestId = noteMergeRequestId;
        }
    }
}