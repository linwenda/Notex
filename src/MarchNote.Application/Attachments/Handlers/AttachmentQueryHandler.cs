using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Attachments.Queries;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Domain.Attachments;
using MarchNote.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace MarchNote.Application.Attachments.Handlers
{
    public class AttachmentQueryHandler : IQueryHandler<GetAttachmentQuery, MarchNoteResponse<AttachmentDto>>
    {
        private readonly IRepository<Attachment> _attachmentRepository;

        public AttachmentQueryHandler(IRepository<Attachment> attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
        }

        public async Task<MarchNoteResponse<AttachmentDto>> Handle(GetAttachmentQuery request,
            CancellationToken cancellationToken)
        {
            return new MarchNoteResponse<AttachmentDto>(
                await _attachmentRepository.Entities
                    .Where(a => a.Id == new AttachmentId(request.AttachmentId))
                    .Select(a => new AttachmentDto
                    {
                        Path = a.Path,
                        ContentType = a.ContentType
                    }).FirstOrDefaultAsync(cancellationToken));
        }
    }
}