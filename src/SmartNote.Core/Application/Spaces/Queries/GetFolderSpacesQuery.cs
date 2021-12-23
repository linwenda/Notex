using SmartNote.Core.Application.Spaces.Commands;

namespace SmartNote.Core.Application.Spaces.Queries
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