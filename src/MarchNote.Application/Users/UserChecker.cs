using System.Linq;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Users;

namespace MarchNote.Application.Users
{
    public class UserChecker : IUserChecker
    {
        private readonly IRepository<User> _userRepository;

        public UserChecker(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsUniqueEmail(string email)
        {
            return !_userRepository.Queryable.Any(u => u.Email == email);
        }
    }
}