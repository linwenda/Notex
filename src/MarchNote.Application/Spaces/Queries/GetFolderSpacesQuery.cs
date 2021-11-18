using System;
using System.Collections.Generic;
using MarchNote.Application.Configuration.Queries;

namespace MarchNote.Application.Spaces.Queries
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