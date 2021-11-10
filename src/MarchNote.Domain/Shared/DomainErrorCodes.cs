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
        public const string NoteHasBeenArchived = "30004";

        //Comment
        //public const string OnlyAuthorOfCommentOrNoteMemberCanDelete = "30001";

        #endregion
    }
}