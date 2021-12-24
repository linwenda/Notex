using AutoMapper;
using SmartNote.Application.Users.Queries;
using SmartNote.Domain.Users;

namespace SmartNote.Application.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
    }
}