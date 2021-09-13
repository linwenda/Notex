using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        IQueryHandler<GetCurrentUserQuery, MarchNoteResponse<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        private readonly IRepository<User> _userRepository;

        public UserQueryHandler(
            IMapper mapper,
            IUserContext userContext,
            IRepository<User> userRepository)
        {
            _mapper = mapper;
            _userContext = userContext;
            _userRepository = userRepository;
        }

        public async Task<MarchNoteResponse<IEnumerable<UserDto>>> Handle(GetUsersQuery request,
            CancellationToken cancellationToken)
        {
            var users = await _userRepository.Entities
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new MarchNoteResponse<IEnumerable<UserDto>>(users);
        }

        public async Task<MarchNoteResponse<UserDto>> Handle(GetCurrentUserQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.Entities
                .Where(u => u.Id == _userContext.UserId)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return new MarchNoteResponse<UserDto>(user);
        }
    }
}