using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SmartNote.Core.Application.Dto;
using SmartNote.Core.Domain;
using SmartNote.Core.Domain.Users;
using SmartNote.Core.Shared;

namespace SmartNote.Core.Application.Queries;

public class UserQueries : IUserQueries
{
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;
    private readonly IReadOnlyRepository<User> _userRepository;

    public UserQueries(
        IMapper mapper,
        ICurrentUser currentUser,
        IReadOnlyRepository<User> userRepository)
    {
        _mapper = mapper;
        _currentUser = currentUser;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserDto>> GetListAsync()
    {
        return await _userRepository.Queryable
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<UserDto> GetCurrentUserAsync()
    {
        return await _userRepository.Queryable.Where(u => u.Id == _currentUser.Id)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }
}