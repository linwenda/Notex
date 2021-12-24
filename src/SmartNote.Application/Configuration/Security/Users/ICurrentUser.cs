using SmartNote.Application.Configuration.DependencyInjection;

namespace SmartNote.Application.Configuration.Security.Users
{
    public interface ICurrentUser : IScopedLifetime
    {
        Guid Id { get; }
    }
}