namespace SmartNote.Core.Domain;

public class DomainErrorCodes
{
    //User
    public const string EmailAlreadyExists = "10001";
    public const string IncorrectPassword = "10002";
    
    //Space
    public const string NotAuthorOfTheSpace = "20001";
    public const string SpaceHasBeenDeleted = "20002";
    public const string SpaceNameAlreadyExists = "20003";
    
    //Note
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
    public const string OnlyNoteAuthorOrCreatorCanBeClosed = "30012";
    public const string OnlyNoteAuthorCanBeMerged = "30013";
    public const string InvalidNoteMergeRequest = "30014";
}