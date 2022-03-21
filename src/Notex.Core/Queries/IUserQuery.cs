using Notex.Core.Aggregates.Users.ReadModels;

namespace Notex.Core.Queries;

public interface IUserQuery
{
    Task<UserDetail> GetUserAsync(Guid id);
    Task<UserDetail> GetCurrentUserAsync();
}