using Funzone.BuildingBlocks.Domain;
using Funzone.PhotoAlbums.Domain.Albums.Exceptions;
using Funzone.PhotoAlbums.Domain.SharedKenel;
using Funzone.PhotoAlbums.Domain.Users;
using System;

namespace Funzone.PhotoAlbums.Domain.Albums
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

        private Album(string name, UserId userId)
        {
            this.Id = new AlbumId(Guid.NewGuid());
            this.Name = name;
            this.UserId = userId;
            this.Visibility = Visibility.Public;
        }

        public static Album Create(
            string name,
            UserId userId,
            IAlbumCounter albumCounter)
        {
            if (albumCounter.CountAlbumsWithName(name, userId) > 0)
            {
                throw new AlbumNameMustBeUniqueException(name);
            }

            return new Album(name, userId);
        }
    }
}