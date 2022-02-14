using Microsoft.EntityFrameworkCore;
using SmartNote.Domain.Users;

namespace SmartNote.Application.Users;

public class UserChecker : IUserChecker
{
    private readonly IUserRepository _userRepository;

    public UserChecker(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> IsUniqueEmail(string email)
    {
        return !await _userRepository.Queryable.AnyAsync(u => u.Email == email);
    }

    public bool IsCorrectPassword(string hashedPassword, string inputPassword)
    {
        return PasswordManager.VerifyHashedPassword(hashedPassword, inputPassword);
    }
}