using Microsoft.EntityFrameworkCore;
using Notex.Core.Aggregates;
using Notex.Core.Aggregates.Users.ReadModels;
using Notex.Core.Configuration;
using Notex.Core.Lifetimes;

namespace Notex.Core.Queries;

public class UserQuery : IUserQuery, IScopedLifetime
{
    private readonly ICurrentUser _currentUser;
    private readonly IReadModelRepository _readModelRepository;

    public UserQuery(ICurrentUser currentUser, IReadModelRepository readModelRepository)
    {
        _currentUser = currentUser;
        _readModelRepository = readModelRepository;
    }

    public async Task<UserDetail> GetUserAsync(Guid id)
    {
        return await _readModelRepository.Query<UserDetail>()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<UserDetail> GetCurrentUserAsync()
    {
        return await _readModelRepository.Query<UserDetail>()
            .FirstOrDefaultAsync(u => u.Id == _currentUser.Id);
    }
}