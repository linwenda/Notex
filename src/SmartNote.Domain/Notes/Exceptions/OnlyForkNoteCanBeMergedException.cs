using SmartNote.Core.Ddd;

namespace SmartNote.Domain.Notes.Exceptions
{
    public class OnlyForkNoteCanBeMergedException : BusinessException
    {
        public OnlyForkNoteCanBeMergedException() : base(DomainErrorCodes.OnlyForkNoteCanBeMerged,
            "Only fork note can be merged")
        {
        }
    }
}