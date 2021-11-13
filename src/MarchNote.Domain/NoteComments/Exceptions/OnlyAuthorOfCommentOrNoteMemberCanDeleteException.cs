﻿using MarchNote.Domain.Shared;

namespace MarchNote.Domain.NoteComments.Exceptions
{
    public class OnlyAuthorOfCommentOrNoteMemberCanDeleteException : BusinessException
    {
        public OnlyAuthorOfCommentOrNoteMemberCanDeleteException() : base(
            DomainErrorCodes.OnlyAuthorOfCommentOrNoteMemberCanDelete,
            "Only author of comment or note member can delete")
        {
        }
    }
}