using SmartNote.Application.Configuration.Queries;
using SmartNote.Application.Spaces.Commands;

namespace SmartNote.Application.Spaces.Queries
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