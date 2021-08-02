using System;
using System.Collections.Generic;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Spaces.Queries
{
    public class GetFolderSpacesQuery : IQuery<MarchNoteResponse<IEnumerable<SpaceDto>>>
    {
        public Guid SpaceId { get; }

        public GetFolderSpacesQuery(Guid spaceId)
        {
            SpaceId = spaceId;
        }
    }
}