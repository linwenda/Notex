using System;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Shared;

namespace MarchNote.Domain.NoteComments.Exceptions
{
    public class NoteCommentHasBeenDeletedException : BusinessNewException
    {
        public NoteCommentHasBeenDeletedException() : base(
            DomainErrorCodes.NoteCommentHasBeenDeleted, "Comment has been deleted")
        {
        }
    }
}