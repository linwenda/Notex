using AutoMapper;
using SmartNote.Core.Application.NoteComments.Queries;
using SmartNote.Core.Domain.NoteComments;

namespace SmartNote.Core.Application.NoteComments
{
    public class NoteCommentProfile : Profile
    {
        public NoteCommentProfile()
        {
            CreateMap<NoteComment, NoteCommentDto>();
        }
    }
}