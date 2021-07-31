using System.Collections.Generic;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Spaces.Queries
{
    public class GetSpacesQuery : IQuery<MarchNoteResponse<IEnumerable<SpaceDto>>>
    {
    }
}