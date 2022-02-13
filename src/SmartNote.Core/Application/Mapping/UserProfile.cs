using AutoMapper;
using SmartNote.Core.Application.Dto;
using SmartNote.Core.Domain.Users;

namespace SmartNote.Core.Application.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
    }
}