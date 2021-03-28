using System;
using Funzone.BuildingBlocks.Domain;
using Funzone.Services.Albums.Domain.PhotoAlbums.Rules;
using Funzone.Services.Albums.Domain.SharedKernel;
using Funzone.Services.Albums.Domain.Users;

namespace Funzone.Services.Albums.Domain.PhotoAlbums
{
    public class Album : Entity, IAggregateRoot
    {
        public AlbumId Id { get; private set; }
        public string Name { get; private set; }
        public UserId UserId { get; private set; }
        public Visibility Visibility { get; private set; }

        //Only for EF
        private Album()
        {
        }

        private Album(
            IAlbumCounter albumCounter,
            string name,
            UserId userId)
        {
            CheckRule(new AlbumNameMustBeUniqueRule(albumCounter, userId, name));
            CheckRule(new Only10AlbumsCanBeAddedRuleWithMember(albumCounter, userId));

            this.Id = new AlbumId(Guid.NewGuid());
            this.Name = name;
            this.UserId = userId;
            this.Visibility = Visibility.Public;
        }

        public static Album Create(
            IAlbumCounter albumCounter,
            string name,
            UserId userId)
        {
            return new Album(albumCounter,name, userId);
        }
    }
}