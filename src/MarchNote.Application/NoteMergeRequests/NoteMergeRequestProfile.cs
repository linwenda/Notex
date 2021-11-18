using AutoMapper;
using MarchNote.Application.NoteMergeRequests.Queries;
using MarchNote.Domain.NoteMergeRequests;

namespace MarchNote.Application.NoteMergeRequests
{
    public class NoteMergeRequestProfile : Profile
    {
        public NoteMergeRequestProfile()
        {
            CreateMap<NoteMergeRequest, NoteMergeRequestDto>();
        }
    }
}