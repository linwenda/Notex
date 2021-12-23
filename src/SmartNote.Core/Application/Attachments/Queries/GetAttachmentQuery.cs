namespace SmartNote.Core.Application.Attachments.Queries
{
    public class GetAttachmentQuery : IQuery<AttachmentDto>
    {
        public Guid AttachmentId { get; }

        public GetAttachmentQuery(Guid attachmentId)
        {
            AttachmentId = attachmentId;
        }
    }
}