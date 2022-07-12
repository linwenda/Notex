using AutoMapper;
using Notex.Core.Domain.Spaces.ReadModels;
using Notex.Messages.Spaces;

namespace Notex.Core.Profiles;

public class SpaceProfile : Profile
{
    public SpaceProfile()
    {
        CreateMap<SpaceDetail, SpaceDto>();
    }
}