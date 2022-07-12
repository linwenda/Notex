using Notex.Core.Exceptions;

namespace Notex.Core.Domain.Notes.Exceptions;

public class NoteHasBeenDeletedException : BusinessException
{
    public NoteHasBeenDeletedException() : base("Note has been deleted")
    {
    }
}