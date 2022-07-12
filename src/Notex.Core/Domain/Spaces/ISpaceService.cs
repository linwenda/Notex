using Notex.Core.DependencyInjection;
using Notex.Messages.Shared;
using Notex.Messages.Spaces;

namespace Notex.Core.Domain.Spaces;

public interface ISpaceService : IScopedLifetime
{
    Task<Space> CreateSpaceAsync(Guid userId, string name, string backgroundImage, Visibility visibility);
    Task UpdateSpaceAsync(Space space, string name, string backgroundImage, Visibility visibility);
}