using AutoMapper;
using SmartNote.Application.Spaces.Commands;
using SmartNote.Domain.Spaces;

namespace SmartNote.Application.Spaces
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