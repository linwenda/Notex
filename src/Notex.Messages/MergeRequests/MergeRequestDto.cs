namespace Notex.Messages.MergeRequests;

public class MergeRequestDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid SourceNoteId { get; set; }
    public Guid DestinationNoteId { get; set; }
    public Guid? ReviewerId { get; set; }
    public DateTime? ReviewTime { get; set; }
    public MergeRequestStatus Status { get; set; }
    public Guid CreatorId { get; set; }
    public DateTime CreationTime { get; set; }
    public Guid? LastModifierId { get; set; }
    public DateTime? LastModificationTime { get; set; }
}