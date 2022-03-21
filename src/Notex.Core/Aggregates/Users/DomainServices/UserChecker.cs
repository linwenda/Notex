using Notex.Core.Aggregates.Users.ReadModels;
using Notex.Core.Lifetimes;

namespace Notex.Core.Aggregates.Users.DomainServices;

public class UserChecker : IUserChecker, IScopedLifetime
{
    private readonly IReadModelRepository _readModelRepository;

    public UserChecker(IReadModelRepository readModelRepository)
    {
        _readModelRepository = readModelRepository;
    }

    public bool IsUniqueEmail(string email)
    {
        return !_readModelRepository.Query<UserDetail>().Any(u => u.Email == email);
    }
}