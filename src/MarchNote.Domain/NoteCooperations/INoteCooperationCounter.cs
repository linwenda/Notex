using System;
using System.Threading.Tasks;
using MarchNote.Domain.Notes;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.NoteCooperations
{
    public interface INoteCooperationCounter : IDomainService
    {
        Task<int> CountPendingAsync(Guid userId, NoteId noteId);
    }
}