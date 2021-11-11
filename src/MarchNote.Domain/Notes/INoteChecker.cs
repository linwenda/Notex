using System;
using System.Threading.Tasks;

namespace MarchNote.Domain.Notes
{
    public interface INoteChecker
    {
        Task<bool> IsAuthorAsync(Guid noteId, Guid userId);
    }
}