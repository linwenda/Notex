using Funzone.BuildingBlocks.Domain;
using Funzone.Services.Albums.Domain.Users;

namespace Funzone.Services.Albums.Domain.Albums.Rules
{
    public class AlbumCountLimitedRule : IBusinessRule
    {
        private readonly IAlbumCounter _albumCounter;
        private readonly UserId _userId;
        private readonly int _limitCount;

        public AlbumCountLimitedRule(
            IAlbumCounter albumCounter,
            UserId userId,
            int limitCount)
        {
            _albumCounter = albumCounter;
            _userId = userId;
            _limitCount = limitCount;
        }

        public bool IsBroken()
        {
            return _albumCounter.CountAlbumsWithUserId(_userId) > _limitCount;
        }

        public string Message => $"Only {_limitCount} albums can be added.";
    }
}