using Notex.Core.Exceptions;

namespace Notex.Core.Aggregates.Notes.Exceptions;

public class NoteHaveNotBeenPublishedException : BusinessException
{
    public NoteHaveNotBeenPublishedException() : base("Note have note been published")
    {
    }
}