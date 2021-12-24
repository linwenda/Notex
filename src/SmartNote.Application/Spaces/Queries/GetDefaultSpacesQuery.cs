using SmartNote.Application.Configuration.Queries;
using SmartNote.Application.Spaces.Commands;

namespace SmartNote.Application.Spaces.Queries
{
    public class GetDefaultSpacesQuery : IQuery<IEnumerable<SpaceDto>>
    {
    }
}