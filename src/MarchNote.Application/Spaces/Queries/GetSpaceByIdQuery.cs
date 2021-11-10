using System;
using MarchNote.Application.Configuration.Queries;

namespace MarchNote.Application.Spaces.Queries
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