using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Users.Queries;
using Microsoft.EntityFrameworkCore;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Users;

namespace MarchNote.Application.Users.Handlers
{
    public class UserQueryHandler :
        IQueryHandler<GetUsersQuery, IEnumerable<UserDto>>,
        IQueryHandler<GetCurrentUserQuery, UserDto>
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

        public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request,
            CancellationToken cancellationToken)
        {
            return await _userRepository.Queryable
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<UserDto> Handle(GetCurrentUserQuery request,
            CancellationToken cancellationToken)
        {
            return await _userRepository.Queryable
                .Where(u => u.Id == _userContext.UserId)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}