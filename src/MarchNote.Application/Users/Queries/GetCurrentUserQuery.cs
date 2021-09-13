using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Users.Queries
{
    public class GetCurrentUserQuery : IQuery<MarchNoteResponse<UserDto>>
    {
    }
}