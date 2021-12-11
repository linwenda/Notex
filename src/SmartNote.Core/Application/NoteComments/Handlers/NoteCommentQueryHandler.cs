using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SmartNote.Core.Application.NoteComments.Contracts;
using SmartNote.Core.Domain;
using SmartNote.Core.Domain.NoteComments;

namespace SmartNote.Core.Application.NoteComments.Handlers
{
    public class NoteCommentQueryHandler :
        IQueryHandler<GetNoteCommentsQuery, IEnumerable<NoteCommentDto>>,
        IQueryHandler<GetNoteCommentByIdQuery, NoteCommentDto>
    {
        private readonly IRepository<NoteComment> _commentRepository;
        private readonly IMapper _mapper;

        public NoteCommentQueryHandler(IRepository<NoteComment> commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
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