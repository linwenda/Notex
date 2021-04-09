using Funzone.BuildingBlocks.Domain;
using Funzone.Services.Albums.Domain.Users;

namespace Funzone.Services.Albums.Domain.Albums.Rules
{
    public class AlbumPictureCanBeAddedOnlyByAuthorRule : IBusinessRule
    {
        private readonly UserId _authorId;
        private readonly UserId _addUserId;

        public AlbumPictureCanBeAddedOnlyByAuthorRule(UserId authorId, UserId addUserId)
        {
            _authorId = authorId;
            _addUserId = addUserId;
        }

        public bool IsBroken() => _authorId != _addUserId;

        public string Message => "Only the album author of a picture can add it.";
    }
}