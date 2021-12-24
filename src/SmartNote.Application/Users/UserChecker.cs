using SmartNote.Domain;
using SmartNote.Domain.Users;

namespace SmartNote.Application.Users;

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

    public bool IsCorrectPassword(User user, string inputPassword)
    {
        return PasswordManager.VerifyHashedPassword(user.Password, inputPassword);
    }
}