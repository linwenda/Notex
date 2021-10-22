using System;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Spaces.Queries
{
    public class GetSpaceByIdQuery : IQuery<MarchNoteResponse<SpaceDto>>
    {
        public Guid SpaceId { get; }

        public GetSpaceByIdQuery(Guid spaceId)
        {
            SpaceId = spaceId;
        }
    }
}