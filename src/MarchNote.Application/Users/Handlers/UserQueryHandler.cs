using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Application.Users.Queries;
using Microsoft.EntityFrameworkCore;
using MarchNote.Domain;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Users;

namespace MarchNote.Application.Users.Handlers
{
    public class UserQueryHandler :
        IQueryHandler<GetUsersQuery, MarchNoteResponse<IEnumerable<UserDto>>>,
        IQueryHandler<GetUserByIdQuery, MarchNoteResponse<UserDto>>
    {
        private readonly IRepository<User> _userRepository;

        public UserQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<MarchNoteResponse<IEnumerable<UserDto>>> Handle(GetUsersQuery request,
            CancellationToken cancellationToken)
        {
            var users = await _userRepository.Entities
                .Select(u => new UserDto
                {
                    Id = u.Id.Value,
                    Email = u.Email,
                    IsActive = u.IsActive,
                    NickName = u.NickName,
                    RegisteredAt = u.RegisteredAt
                }).ToListAsync(cancellationToken);

            return new MarchNoteResponse<IEnumerable<UserDto>>(users);
        }

        public async Task<MarchNoteResponse<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Entities
                .Select(u => new UserDto
                {
                    Id = u.Id.Value,
                    Email = u.Email,
                    IsActive = u.IsActive,
                    NickName = u.NickName,
                    RegisteredAt = u.RegisteredAt
                }).FirstOrDefaultAsync(cancellationToken);

            return new MarchNoteResponse<UserDto>(user);
        }
    }
}