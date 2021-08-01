using System.Collections.Generic;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Spaces.Queries
{
    public class GetDefaultSpacesQuery : IQuery<MarchNoteResponse<IEnumerable<SpaceDto>>>
    {
    }
}