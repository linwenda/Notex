namespace Notex.Core.Identity;

public interface ICurrentUser
{
    Guid Id { get; }
}