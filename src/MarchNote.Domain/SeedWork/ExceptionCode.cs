namespace MarchNote.Domain.SeedWork
{
    public enum ExceptionCode
    {
        BusinessValidationFailed = 50000,

        #region User 50100~50199

        UserEmailExists = 50100,
        UserPasswordIncorrect = 50101,
        UserNickNameExists = 50102,

        #endregion

        #region Note 50200~50299

        NoteHasBeenDeleted = 50200,
        NotePermissionDenied = 50201,
        NoteUserHasBeenJoined = 50202,
        NoteMemberHasBeenRemoved = 50203,
        NoteStatusMustBePublished = 50204,
        NotePublishOnlyByMain = 50205,
        NoteMergeOnlyByDraftOutNote = 50206,
        NoteCooperationWriterExists = 50207,
        NoteCanBeAddedOnlyBySpaceAuthor = 50208,
        
        #endregion

        #region Cooperation 50300~50399

        CooperationApplicationInProgress = 50300,
        CooperationOnlyNoteOwnerCanBeApproved = 50301,
        CooperationOnlyPendingCanBeApproved = 50302,
        CooperationOnlyPendingCanBeRejected = 50303,

        #endregion

        #region Comment 50400

        CommentHasBeenDeleted = 50400,
        CommentCanBeDeletedOnlyByAuthorOrMember = 50401,

        #endregion

        #region Space 50500

        SpaceHasBeenDeleted = 50500,
        SpaceCanBeOperatedOnlyByAuthor = 50501,
        SpaceFolderCanBeOperatedOnlyByAuthor = 50502,
        SpaceOnlyFolderTypeCanBeMoved = 50503,
        SpaceCannotMovingOneself = 50504

        #endregion
    }
}