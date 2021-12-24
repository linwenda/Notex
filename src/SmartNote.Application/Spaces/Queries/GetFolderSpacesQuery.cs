using SmartNote.Application.Configuration.Queries;
using SmartNote.Application.Spaces.Commands;

namespace SmartNote.Application.Spaces.Queries
{
    public class GetFolderSpacesQuery : IQuery<IEnumerable<SpaceDto>>
    {
        public Guid SpaceId { get; }

        public GetFolderSpacesQuery(Guid spaceId)
        {
            SpaceId = spaceId;
        }
    }
}