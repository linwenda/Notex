using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Attachments.Queries;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Domain.Attachments;
using MarchNote.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace MarchNote.Application.Attachments.Handlers
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