using SmartNote.Core.Domain.NoteMergeRequests;

namespace SmartNote.Core.Application.NoteMergeRequests.Queries
{
    public class NoteMergeRequestDto
    {
        public Guid CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid NoteId { get; set; }
        public Guid? ReviewerId { get; set; }
        public DateTime? ReviewTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public NoteMergeRequestStatus Status { get; set; }
    }
}