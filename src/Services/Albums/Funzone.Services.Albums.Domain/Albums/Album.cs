using Funzone.BuildingBlocks.Domain;
using Funzone.Services.Albums.Domain.Albums.Rules;
using Funzone.Services.Albums.Domain.SharedKernel;
using Funzone.Services.Albums.Domain.Users;
using System;

namespace Funzone.Services.Albums.Domain.Albums
{
    public class Album : Entity, IAggregateRoot
    {
        public AlbumId Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public UserId UserId { get; private set; }
        public Visibility Visibility { get; private set; }
        public DateTime CreatedTime { get; private set; }

        //Only for EF
        private Album()
        {
        }

        private Album(
            IAlbumCounter albumCounter,
            UserId userId,
            string title,
            string description)
        {
            CheckRule(new AlbumNameMustBeUniqueRule(albumCounter, userId, title));
            CheckRule(new Only10AlbumsCanBeAddedRuleWithMember(albumCounter, userId));

            Id = new AlbumId(Guid.NewGuid());
            UserId = userId;
            Title = title;
            Description = description;
            Visibility = Visibility.Public;
            CreatedTime = DateTime.UtcNow;
        }

        public static Album Create(
            IAlbumCounter albumCounter,
            UserId userId,
            string title,
            string description)
        {
            return new Album(albumCounter, userId, title, description);
        }

        public void MakePublic()
        {
            if (Visibility == Visibility.Public)
                return;

            Visibility = Visibility.Public;
        }

        public void MakePrivate()
        {
            if (Visibility == Visibility.Private)
                return;

            Visibility = Visibility.Private;
        }
    }
}