using AutoMapper;
using SmartNote.Core.Application.NoteMergeRequests.Contracts;
using SmartNote.Core.Domain.NoteMergeRequests;

namespace SmartNote.Core.Application.NoteMergeRequests
{
    public class NoteMergeRequestProfile : Profile
    {
        public NoteMergeRequestProfile()
        {
            CreateMap<NoteMergeRequest, NoteMergeRequestDto>();
        }
    }
}