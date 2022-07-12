using AutoMapper;
using Notex.Core.Domain.MergeRequests.ReadModels;
using Notex.Messages.MergeRequests;

namespace Notex.Core.Profiles;

public class MergeRequestProfile : Profile
{
    public MergeRequestProfile()
    {
        CreateMap<MergeRequestDetail, MergeRequestDto>();
    }
}