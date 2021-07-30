using MarchNote.Domain.NoteAggregate;
using MarchNote.Domain.Users;

namespace MarchNote.Domain.NoteCooperations
{
    public interface INoteCooperationCounter
    {
        int CountPending(UserId userId, NoteId noteId);
    }
}