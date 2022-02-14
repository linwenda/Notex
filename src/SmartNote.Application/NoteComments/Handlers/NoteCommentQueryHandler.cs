using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SmartNote.Application.Configuration.Queries;
using SmartNote.Application.NoteComments.Queries;
using SmartNote.Domain.NoteComments;

namespace SmartNote.Application.NoteComments.Handlers
{
    public class NoteCommentQueryHandler :
        IQueryHandler<GetNoteCommentsQuery, IEnumerable<NoteCommentDto>>,
        IQueryHandler<GetNoteCommentByIdQuery, NoteCommentDto>
    {
        private readonly IMapper _mapper;
        private readonly INoteCommentRepository _commentRepository;

        public NoteCommentQueryHandler(
            IMapper mapper,
            INoteCommentRepository commentRepository)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<NoteCommentDto>> Handle(GetNoteCommentsQuery request,
            CancellationToken cancellationToken)
        {
            return await _commentRepository.Queryable
                .Where(c => c.NoteId == request.NoteId)
                .ProjectTo<NoteCommentDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<NoteCommentDto> Handle(GetNoteCommentByIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _commentRepository.Queryable
                .Where(c => c.Id == request.CommentId)
                .ProjectTo<NoteCommentDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}