using AutoMapper;
using SmartNote.Core.Application.NoteMergeRequests.Queries;
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