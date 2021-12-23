using SmartNote.Core.Application.Spaces.Commands;

namespace SmartNote.Core.Application.Spaces.Queries
{
    public class GetSpaceByIdQuery : IQuery<SpaceDto>
    {
        public Guid SpaceId { get; }

        public GetSpaceByIdQuery(Guid spaceId)
        {
            SpaceId = spaceId;
        }
    }
}