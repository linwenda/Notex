namespace MarchNote.Domain.Shared
{
    //Localization -- Json
    public static class DomainErrorCodes
    {
        #region User

        public const string EmailAlreadyExists = "10001";
        public const string IncorrectEmailOrPassword = "10002";

        #endregion

        #region Space

        public const string NotAuthorOfTheSpace = "20001";
        public const string SpaceHasBeenDeleted = "20002";
        public const string SpaceNameAlreadyExists = "20003";

        #endregion

        #region Note

        public const string NotAuthorOfTheNote = "30001";
        public const string NotMemberOfTheNote = "30002";
        public const string NoteHasBeenDeleted = "30003";
        public const string InvalidNoteStatus = "30004";
        public const string NoteCommentHasBeenDeleted = "30005";
        public const string OnlyAuthorOfCommentOrNoteMemberCanDelete = "30006";
        public const string InvalidCooperationStatus = "30007";
        public const string CooperationApplicationInProgress = "30008";
        public const string OnlyForkNoteCanBeMerged = "30009";
        public const string UserHasBeenJoinedThisNoteCooperation = "30010";
        public const string NotePermissionDenied = "30011";

        #endregion
    }
}