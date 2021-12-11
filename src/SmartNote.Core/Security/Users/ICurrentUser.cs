using SmartNote.Core.DependencyInjection;

namespace SmartNote.Core.Security.Users
{
    public interface ICurrentUser : IScopedLifetime
    {
        Guid Id { get; }
    }
}