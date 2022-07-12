using AutoMapper;
using Notex.Core.Domain.Comments.ReadModels;
using Notex.Messages.Comments;

namespace Notex.Core.Profiles;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<CommentDetail, CommentDto>();
    }
}