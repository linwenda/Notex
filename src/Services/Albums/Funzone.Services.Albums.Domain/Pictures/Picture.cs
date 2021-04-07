using Funzone.BuildingBlocks.Domain;
using Funzone.Services.Albums.Domain.Albums;
using Funzone.Services.Albums.Domain.Users;
using Funzone.Services.Albums.Domain.Pictures.Rules;
using System;

namespace Funzone.Services.Albums.Domain.Pictures
{
    public class Picture : Entity, IAggregateRoot
    {
        public PictureId Id { get; private set; }
        public AlbumId AlbumId { get; private set; }
        public UserId UserId { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public string Title { get; private set; }
        public string Link { get; private set; }
        public string ThumbnailLink { get; private set; }
        public string Description { get; private set; }

        //Only for EF
        private Picture()
        {
            
        }

        private Picture(
            IPictureCounter pictureCounter,
            AlbumId albumId,
            UserId userId,
            string title,
            string link,
            string thumbnailLink,
            string description)
        {
            CheckRule(new PictureCountLimitedRule(pictureCounter, albumId, 100));
            
            Id = new PictureId(Guid.NewGuid());
            AlbumId = albumId;
            UserId = userId;
            Title = title;
            Link = link;
            ThumbnailLink = thumbnailLink;
            Description = description;
            CreatedTime = DateTime.UtcNow;
        }

        public static Picture Add(
            IPictureCounter pictureCounter,
            AlbumId albumId,
            UserId userId,
            string title,
            string link,
            string thumbnailLink,
            string description)
        {
            return new Picture(
                pictureCounter,
                albumId,
                userId,
                title,
                link,
                thumbnailLink,
                description);
        }
    }
}