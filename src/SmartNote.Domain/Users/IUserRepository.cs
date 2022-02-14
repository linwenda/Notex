using SmartNote.Core.Ddd;

namespace SmartNote.Domain.Users;

public interface IUserRepository : IRepository<User, Guid>
{
}