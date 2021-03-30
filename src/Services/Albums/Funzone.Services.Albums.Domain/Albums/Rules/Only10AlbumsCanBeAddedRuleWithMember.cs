using Funzone.BuildingBlocks.Domain;
using Funzone.Services.Albums.Domain.Users;

namespace Funzone.Services.Albums.Domain.Albums.Rules
{
    public class Only10AlbumsCanBeAddedRuleWithMember : IBusinessRule
    {
        private readonly IAlbumCounter _albumCounter;
        private readonly UserId _userId;

        public Only10AlbumsCanBeAddedRuleWithMember(
            IAlbumCounter albumCounter,
            UserId userId)
        {
            _albumCounter = albumCounter;
            _userId = userId;
        }
        
        public bool IsBroken()
        {
            return _albumCounter.CountAlbumsWithUserId(_userId) > 10;
        }

        public string Message => "Only 10 albums can be added.";
    }
}