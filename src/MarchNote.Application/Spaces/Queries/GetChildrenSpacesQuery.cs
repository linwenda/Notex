using System;
using System.Collections.Generic;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Spaces.Queries
{
    public class GetChildrenSpacesQuery : IQuery<MarchNoteResponse<IEnumerable<SpaceDto>>>
    {
        public Guid SpaceId { get; }

        public GetChildrenSpacesQuery(Guid spaceId)
        {
            SpaceId = spaceId;
        }
    }
}