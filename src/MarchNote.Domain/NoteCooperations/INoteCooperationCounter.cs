using System;
using System.Threading.Tasks;
using MarchNote.Domain.Notes;
using MarchNote.Domain.Shared;

namespace MarchNote.Domain.NoteCooperations
{
    public interface INoteCooperationCounter : IDomainService
    {
        Task<int> CountPendingAsync(Guid userId, Guid noteId);
    }
}