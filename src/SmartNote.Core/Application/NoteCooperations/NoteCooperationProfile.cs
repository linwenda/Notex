using AutoMapper;
using SmartNote.Core.Application.NoteCooperations.Queries;
using SmartNote.Core.Domain.NoteCooperations;

namespace SmartNote.Core.Application.NoteCooperations
{
    public class NoteCooperationProfile : Profile
    {
        public NoteCooperationProfile()
        {
            CreateMap<NoteCooperation, NoteCooperationDto>();
        }
    }
}