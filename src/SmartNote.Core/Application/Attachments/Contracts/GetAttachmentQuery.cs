using SmartNote.Core.Application.Attachments.Contracts;

namespace SmartNote.Core.Application.Attachments.Contrancts
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