using System;
using System.Threading.Tasks;
using MarchNote.Domain.Notes;

namespace MarchNote.Application.Notes
{
    public interface INoteDataProvider
    {
        Task<NoteMemberGroup> GetMemberList(Guid noteId);
    }
}