using System.Threading.Tasks;
using MarchNote.Domain.Notes;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Users;

namespace MarchNote.Domain.NoteCooperations
{
    public interface INoteCooperationCounter : IDomainService
    {
        Task<int> CountPendingAsync(UserId userId, NoteId noteId);
    }
}