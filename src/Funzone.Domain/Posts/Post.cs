using System;
using Funzone.Domain.Posts.Rules;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;
using Funzone.Domain.ZoneUsers;

namespace Funzone.Domain.Posts
{
    public class Post : Entity, IAggregateRoot
    {
        public PostId Id { get; private set; }
        public ZoneId ZoneId { get; private set; }
        public UserId AuthorId { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public DateTime PostedTime { get; private set; }
        public DateTime? EditedTime { get; private set; }
        public bool IsDeleted { get; private set; }

        private Post()
        {
        }

        internal Post(
            ZoneId zoneId,
            UserId userId,
            string title,
            string content)
        {
            ZoneId = zoneId;
            AuthorId = userId;
            Title = title;
            Content = content;

            Id = new PostId(Guid.NewGuid());
            PostedTime = Clock.Now;
        }

        public static Post Create(ZoneUser zoneUser, string title, string content)
        {
            return new Post(zoneUser.ZoneId, zoneUser.UserId, title, content);
        }

        public void Edit(UserId editorId, string title, string content)
        {
            CheckRule(new PostCanBeEditedOnlyByAuthorRule(AuthorId, editorId));
            Title = title;
            Content = content;
            EditedTime = Clock.Now;
        }

        public Post GetSnapShot(ZoneUser zoneUser, string title, string content)
        {
            return new Post(zoneUser, title, content);
        }
    }
}