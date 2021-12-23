using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SmartNote.Core.Application.Users.Queries;
using SmartNote.Core.Domain;
using SmartNote.Core.Domain.Users;
using SmartNote.Core.Security.Users;

namespace SmartNote.Core.Application.Users.Handlers;

public class UserQueryHandler :
    IQueryHandler<GetUsersQuery, IEnumerable<UserDto>>,
    IQueryHandler<GetCurrentUserQuery, UserDto>
{
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;
    private readonly IRepository<User> _userRepository;

    public UserQueryHandler(
        IMapper mapper,
        ICurrentUser currentUser,
        IRepository<User> userRepository)
    {
        _mapper = mapper;
        _currentUser = currentUser;
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
            .Where(u => u.Id == _currentUser.Id)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}