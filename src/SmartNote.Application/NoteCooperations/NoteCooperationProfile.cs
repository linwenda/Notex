using AutoMapper;
using SmartNote.Application.NoteCooperations.Queries;
using SmartNote.Domain.NoteCooperations;

namespace SmartNote.Application.NoteCooperations
{
    public class NoteCooperationProfile : Profile
    {
        public NoteCooperationProfile()
        {
            CreateMap<NoteCooperation, NoteCooperationDto>();
        }
    }
}