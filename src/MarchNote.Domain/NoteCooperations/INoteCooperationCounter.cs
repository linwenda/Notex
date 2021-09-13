using MarchNote.Domain.Notes;
using MarchNote.Domain.Users;

namespace MarchNote.Domain.NoteCooperations
{
    public interface INoteCooperationCounter
    {
        int CountPending(UserId userId, NoteId noteId);
    }
}