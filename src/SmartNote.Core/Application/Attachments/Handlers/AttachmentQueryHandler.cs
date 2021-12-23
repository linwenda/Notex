using Microsoft.EntityFrameworkCore;
using SmartNote.Core.Application.Attachments.Queries;
using SmartNote.Core.Domain;
using SmartNote.Core.Domain.Attachments;

namespace SmartNote.Core.Application.Attachments.Handlers
{
    public class AttachmentQueryHandler : IQueryHandler<GetAttachmentQuery, AttachmentDto>
    {
        private readonly IRepository<Attachment> _attachmentRepository;

        public AttachmentQueryHandler(IRepository<Attachment> attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
        }

        public async Task<AttachmentDto> Handle(GetAttachmentQuery request,
            CancellationToken cancellationToken)
        {
            return await _attachmentRepository.Queryable
                .Where(a => a.Id == request.AttachmentId)
                .Select(a => new AttachmentDto
                {
                    Path = a.Path,
                    ContentType = a.ContentType
                }).FirstOrDefaultAsync(cancellationToken);
        }
    }
}