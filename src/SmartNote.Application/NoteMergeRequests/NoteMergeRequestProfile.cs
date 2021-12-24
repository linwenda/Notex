using AutoMapper;
using SmartNote.Application.NoteMergeRequests.Queries;
using SmartNote.Domain.NoteMergeRequests;

namespace SmartNote.Application.NoteMergeRequests
{
    public class NoteMergeRequestProfile : Profile
    {
        public NoteMergeRequestProfile()
        {
            CreateMap<NoteMergeRequest, NoteMergeRequestDto>();
        }
    }
}