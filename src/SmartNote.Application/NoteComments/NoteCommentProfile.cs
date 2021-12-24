using AutoMapper;
using SmartNote.Application.NoteComments.Queries;
using SmartNote.Domain.NoteComments;

namespace SmartNote.Application.NoteComments
{
    public class NoteCommentProfile : Profile
    {
        public NoteCommentProfile()
        {
            CreateMap<NoteComment, NoteCommentDto>();
        }
    }
}