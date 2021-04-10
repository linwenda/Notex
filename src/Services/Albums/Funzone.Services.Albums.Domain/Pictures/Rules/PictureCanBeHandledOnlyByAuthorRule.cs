using Funzone.BuildingBlocks.Domain;
using Funzone.Services.Albums.Domain.Users;

namespace Funzone.Services.Albums.Domain.Pictures.Rules
{
    public class PictureCanBeHandledOnlyByAuthorRule : IBusinessRule
    {
        private readonly UserId _authorId;
        private readonly UserId _handlerId;

        public PictureCanBeHandledOnlyByAuthorRule(UserId authorId, UserId handlerId)
        {
            _authorId = authorId;
            _handlerId = handlerId;
        }

        public bool IsBroken()
        {
            return _authorId != _handlerId;
        }

        public string Message => "Only the author of a picture can edit it.";
    }
}