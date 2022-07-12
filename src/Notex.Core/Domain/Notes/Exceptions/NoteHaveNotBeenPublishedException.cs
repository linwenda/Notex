using Notex.Core.Exceptions;

namespace Notex.Core.Domain.Notes.Exceptions;

public class NoteHaveNotBeenPublishedException : BusinessException
{
    public NoteHaveNotBeenPublishedException() : base("Note not published")
    {
    }
}