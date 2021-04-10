using Funzone.BuildingBlocks.Domain;
using Funzone.Services.Albums.Domain.Albums;
using Funzone.Services.Albums.Domain.Users;
using System;
using Funzone.Services.Albums.Domain.Albums.Rules;
using Funzone.Services.Albums.Domain.Pictures.Rules;

namespace Funzone.Services.Albums.Domain.Pictures
{
    public class Picture : Entity, IAggregateRoot
    {
        public PictureId Id { get; private set; }
        public AlbumId AlbumId { get; private set; }
        public UserId AuthorId { get; private set; }
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
            AlbumId albumId,
            UserId userId,
            string title,
            string link,
            string thumbnailLink,
            string description)
        {
            Id = new PictureId(Guid.NewGuid());
            AlbumId = albumId;
            AuthorId = userId;
            Title = title;
            Link = link;
            ThumbnailLink = thumbnailLink;
            Description = description;
            CreatedTime = DateTime.UtcNow;
        }

        public static Picture Create(
            AlbumId albumId,
            UserId userId,
            string title,
            string link,
            string thumbnailLink,
            string description)
        {
            return new Picture(
                albumId,
                userId,
                title,
                link,
                thumbnailLink,
                description);
        }

        public void Edit(UserId editorId, string title, string description)
        {
            CheckHandler(editorId);
            
            Title = title;
            Description = description;
        }

        public void CheckHandler(UserId handlerId)
        {
            CheckRule(new PictureCanBeHandledOnlyByAuthorRule(AuthorId, handlerId));
        }
    }
}