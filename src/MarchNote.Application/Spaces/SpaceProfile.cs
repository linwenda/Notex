using AutoMapper;
using MarchNote.Application.Spaces.Queries;
using MarchNote.Domain.Spaces;

namespace MarchNote.Application.Spaces
{
    public class SpaceProfile : Profile
    {
        public SpaceProfile()
        {
            CreateMap<Space, SpaceDto>()
                .ForMember(d => d.BackgroundColor, opt => opt.MapFrom(s => s.Background.Color))
                .ForMember(d => d.BackgroundImageId, opt => opt.MapFrom(s => s.Background.ImageId));
        }
    }
}