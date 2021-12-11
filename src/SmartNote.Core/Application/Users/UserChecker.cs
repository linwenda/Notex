using SmartNote.Core.Domain;
using SmartNote.Core.Domain.Users;

namespace SmartNote.Core.Application.Users;

public class UserChecker : IUserChecker
{
    private readonly IRepository<User> _userRepository;

    public UserChecker(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> IsUniqueEmail(string email)
    {
        return !await _userRepository.AnyAsync(u => u.Email == email);
    }
}