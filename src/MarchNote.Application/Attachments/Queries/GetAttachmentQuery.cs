using System;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Attachments.Queries
{
    public class GetAttachmentQuery : IQuery<MarchNoteResponse<AttachmentDto>>
    {
        public Guid AttachmentId { get; }

        public GetAttachmentQuery(Guid attachmentId)
        {
            AttachmentId = attachmentId;
        }
    }
}