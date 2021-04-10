using Funzone.BuildingBlocks.Domain;

namespace Funzone.Services.Albums.Domain.PictureComment.Rules
{
    public class CommentCanBeDeleteOnlyByAuthorOrPictureAuthorRule : IBusinessRule
    {
        public bool IsBroken()
        {
            throw new System.NotImplementedException();
        }

        public string Message { get; }
    }
}