using AutoMapper;
using MarchNote.Application.Spaces.Queries;
using MarchNote.Domain.Spaces;

namespace MarchNote.Application.Spaces
{
    public class SpaceProfile : Profile
    {
        public SpaceProfile()
        {
            CreateMap<Space, SpaceDto>();
            CreateMap<SpaceFolder, SpaceFolderDto>();
        }
    }
}