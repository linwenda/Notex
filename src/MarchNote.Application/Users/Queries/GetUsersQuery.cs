using System.Collections.Generic;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Users.Queries
{
    public class GetUsersQuery : IQuery<MarchNoteResponse<IEnumerable<UserDto>>>
    {
    }
}