using Notex.Core.Aggregates.Spaces.ReadModels;

namespace Notex.Core.Queries;

public interface ISpaceQuery
{
    Task<SpaceDetail> GetSpaceAsync(Guid id);
    Task<IEnumerable<SpaceDetail>> GetMySpacesAsync();
}