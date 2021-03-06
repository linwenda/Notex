using Funzone.BuildingBlocks.Domain;
using Funzone.PhotoAlbums.Domain.Albums.Exceptions;
using Funzone.PhotoAlbums.Domain.Users;
using System;

namespace Funzone.PhotoAlbums.Domain.Albums
{
    public class Album : Entity, IAggregateRoot
    {
        public AlbumId Id { get; private set; }
        public string Name { get; private set; }
        public UserId UserId { get; private set; }

        //Only for EF
        private Album()
        {
        }

        private Album(string name, UserId userId)
        {
            Id = new AlbumId(Guid.NewGuid());
            Name = name;
            UserId = userId;
        }

        public static Album Create(
            string name, 
            UserId userId,
            IAlbumCounter photoAlbumCounter)
        {
            if (photoAlbumCounter.CountAlbumsWithName(name) > 0)
            {
                throw new AlbumNameMustBeUniqueException(name);
            }

            return new Album(name, userId);
        }
    }
}