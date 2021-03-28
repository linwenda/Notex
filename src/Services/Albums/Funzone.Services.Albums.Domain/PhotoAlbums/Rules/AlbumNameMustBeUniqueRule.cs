using Funzone.BuildingBlocks.Domain;
using Funzone.Services.Albums.Domain.Users;

namespace Funzone.Services.Albums.Domain.PhotoAlbums.Rules
{
    public class AlbumNameMustBeUniqueRule : IBusinessRule
    {
        private readonly IAlbumCounter _albumCounter;
        private readonly UserId _userId;
        private readonly string _name;

        public AlbumNameMustBeUniqueRule(IAlbumCounter albumCounter, UserId userId, string name)
        {
            _albumCounter = albumCounter;
            _userId = userId;
            _name = name;
        }

        public bool IsBroken()
        {
            return _albumCounter.CountAlbumsWithName(_name, _userId) > 0;
        }

        public string Message => "Album with this name already exists.";
    }
}