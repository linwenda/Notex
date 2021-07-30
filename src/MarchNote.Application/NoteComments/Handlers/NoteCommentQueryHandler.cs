using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Application.NoteComments.Queries;
using MarchNote.Domain.NoteComments;
using MarchNote.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace MarchNote.Application.NoteComments.Handlers
{
    public class NoteCommentQueryHandler :
        IQueryHandler<GetNoteCommentsQuery, MarchNoteResponse<IEnumerable<NoteCommentDto>>>,
        IQueryHandler<GetNoteCommentByIdQuery, MarchNoteResponse<NoteCommentDto>>
    {
        private readonly IRepository<NoteComment> _commentRepository;
        private readonly IMapper _mapper;

        public NoteCommentQueryHandler(IRepository<NoteComment> commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<MarchNoteResponse<IEnumerable<NoteCommentDto>>> Handle(GetNoteCommentsQuery request,
            CancellationToken cancellationToken)
        {
            var comments = await _commentRepository.Entities
                .Where(c => c.NoteId == new NoteCommentId(request.NoteId))
                .ProjectTo<NoteCommentDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new MarchNoteResponse<IEnumerable<NoteCommentDto>>(comments);
        }

        public async Task<MarchNoteResponse<NoteCommentDto>> Handle(GetNoteCommentByIdQuery request,
            CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.Entities
                .Where(c => c.Id == new NoteCommentId(request.CommentId))
                .ProjectTo<NoteCommentDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return new MarchNoteResponse<NoteCommentDto>(comment);
        }
    }
}