using System;
using MarchNote.Application.Configuration.Queries;

namespace MarchNote.Application.Attachments.Queries
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