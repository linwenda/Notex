using Funzone.BuildingBlocks.Domain;
using Funzone.Services.Albums.Domain.Albums.Rules;
using Funzone.Services.Albums.Domain.SharedKernel;
using Funzone.Services.Albums.Domain.Users;
using System;
using System.Collections.Generic;
using Funzone.Services.Albums.Domain.Pictures;

namespace Funzone.Services.Albums.Domain.Albums
{
    public class Album : Entity, IAggregateRoot
    {
        public AlbumId Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public UserId AuthorId { get; private set; }
        public Visibility Visibility { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public List<Picture> Pictures { get; private set; }

        //Only for EF
        private Album()
        {
            Pictures = new List<Picture>();
        }

        private Album(
            IAlbumCounter albumCounter,
            UserId userId,
            string title,
            string description)
        {
            CheckRule(new AlbumCountLimitedRule(albumCounter, userId, 10));

            Id = new AlbumId(Guid.NewGuid());
            AuthorId = userId;
            Title = title;
            Description = description;
            Visibility = Visibility.Public;
            CreatedTime = DateTime.UtcNow;

            Pictures = new List<Picture>();
        }

        public static Album Create(
            IAlbumCounter albumCounter,
            UserId userId,
            string title,
            string description)
        {
            return new Album(albumCounter, userId, title, description);
        }

        public void Edit(UserId editorId, string title, string description)
        {
            CheckRule(new AlbumCanBeEditedOnlyByAuthorRule(AuthorId, editorId));

            Title = title;
            Description = description;
        }

        public void ChangeVisibility(UserId editorId, Visibility visibility)
        {
            CheckRule(new AlbumCanBeEditedOnlyByAuthorRule(AuthorId, editorId));

            this.Visibility = visibility;
        }

        //TODO: Check album limit
        public Picture AddPicture(IAlbumCounter albumCounter,
            UserId addUserId,
            string title,
            string link,
            string thumbnailLink,
            string description)
        {
            CheckRule(new AlbumPictureCanBeAddedOnlyByAuthorRule(AuthorId, addUserId));
            CheckRule(new AlbumPicturesCountLimitedRule(albumCounter, Id, 100));

            var picture = Picture.Create(Id, addUserId, title, link, thumbnailLink, description);
            
            Pictures.Add(picture);
            
            return picture;
        }
    }
}