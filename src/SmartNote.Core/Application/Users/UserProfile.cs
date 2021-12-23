using AutoMapper;
using SmartNote.Core.Application.Users.Queries;
using SmartNote.Core.Domain.Users;

namespace SmartNote.Core.Application.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
    }
}