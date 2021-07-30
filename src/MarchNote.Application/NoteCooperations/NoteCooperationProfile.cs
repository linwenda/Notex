using AutoMapper;
using MarchNote.Application.NoteCooperations.Queries;
using MarchNote.Domain.NoteCooperations;

namespace MarchNote.Application.NoteCooperations
{
    public class NoteCooperationProfile : Profile
    {
        public NoteCooperationProfile()
        {
            CreateMap<NoteCooperation, NoteCooperationDto>();
        }
    }
}