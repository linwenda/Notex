using AutoMapper;
using SmartNote.Core.Application.Spaces.Commands;
using SmartNote.Core.Domain.Spaces;

namespace SmartNote.Core.Application.Spaces
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