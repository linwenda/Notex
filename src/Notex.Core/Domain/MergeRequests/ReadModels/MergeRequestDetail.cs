using Notex.Core.Domain.SeedWork;
using Notex.Messages.MergeRequests;

namespace Notex.Core.Domain.MergeRequests.ReadModels;

public class MergeRequestDetail : IEntity
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