using AutoMapper;
using MarchNote.Application.Users.Queries;
using MarchNote.Domain.Users;

namespace MarchNote.Application.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}