using Funzone.BuildingBlocks.Domain;
using Funzone.Services.Albums.Domain.Albums;

namespace Funzone.Services.Albums.Domain.Pictures.Rules
{
    public class PictureCountLimitedRule : IBusinessRule
    {
        private readonly IPictureCounter _pictureCounter;
        private readonly AlbumId _albumId;
        private readonly int _limitCount;

        public PictureCountLimitedRule(
            IPictureCounter pictureCounter,
            AlbumId albumId,
            int limitCount)
        {
            _pictureCounter = pictureCounter;
            _albumId = albumId;
            _limitCount = limitCount;
        }

        public bool IsBroken()
        {
            return _pictureCounter.CountPicturesWithAlbumId(_albumId) > _limitCount;
        }

        public string Message =>$"Only {_limitCount} pictures can be added.";
    }
}