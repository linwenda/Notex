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

    public class AlbumPicturesCountLimitedRule : IBusinessRule
    {
        private readonly IAlbumCounter _albumCounter;
        private readonly AlbumId _albumId;
        private readonly int _limitCount;

        public AlbumPicturesCountLimitedRule(
            IAlbumCounter albumCounter,
            AlbumId albumId,
            int limitCount)
        {
            _albumCounter = albumCounter;
            _albumId = albumId;
            _limitCount = limitCount;
        }

        public bool IsBroken()
        {
            return _albumCounter.CountPicturesWithAlbumId(_albumId) > _limitCount;
        }

        public string Message => $"Only {_limitCount} pictures can be added.";
    }
}