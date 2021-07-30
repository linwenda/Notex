using System;
using System.Threading.Tasks;
using MarchNote.Domain.NoteAggregate;

namespace MarchNote.Application.Notes
{
    public interface INoteDataProvider
    {
        Task<NoteMemberList> GetMemberList(Guid noteId);
    }
}