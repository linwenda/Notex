using Notex.Core.Exceptions;

namespace Notex.Core.Aggregates.Notes.Exceptions;

public class OnlyCloneNoteCanBeMergedException : BusinessException
{
    public OnlyCloneNoteCanBeMergedException() : base("Only clone note can be merged")
    {
    }
}