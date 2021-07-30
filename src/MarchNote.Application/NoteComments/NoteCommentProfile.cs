using AutoMapper;
using MarchNote.Application.NoteComments.Queries;
using MarchNote.Domain.NoteComments;

namespace MarchNote.Application.NoteComments
{
    public class NoteCommentProfile : Profile
    {
        public NoteCommentProfile()
        {
            CreateMap<NoteComment, NoteCommentDto>();
        }
    }
}