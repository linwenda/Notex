using Notex.Core.DependencyInjection;
using Notex.Messages.Identity;

namespace Notex.Core.Identity;

public interface IIdentityService : IScopedLifetime
{
    Task<UserInfo> GetCurrentUserInfoAsync();
    Task<string> GenerateTokenAsync(string userName);
}