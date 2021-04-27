using System;
using System.Collections.Generic;
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
        public PostType Type { get; private set; }
        public PostStatus Status { get; private set; }
        public List<PostReview> PostReviews { get; private set; }

        private Post()
        {
            PostReviews = new List<PostReview>();
        }

        internal Post(
            ZoneId zoneId,
            UserId userId,
            string title,
            string content) : this()
        {
            ZoneId = zoneId;
            AuthorId = userId;
            Title = title;
            Content = content;

            Id = new PostId(Guid.NewGuid());
            PostedTime = Clock.Now;
            Status = PostStatus.WaitingForReview;
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

        public void Delete(UserId deleterId)
        {
            CheckRule(new PostCanBeDeletedOnlyByAuthorRule(AuthorId, deleterId));
            IsDeleted = true;
        }

        public void Approve(ZoneUser zoneUser)
        {
            CheckRule(new PostCanBeReviewedOnlyByModeratorRule(zoneUser));

            Status = PostStatus.Approved;

            PostReviews.Add(
                new PostReview(Id,
                    PostStatus.Approved,
                    zoneUser.UserId));
        }

        public void Reject(ZoneUser zoneUser, string detail)
        {
            CheckRule(new PostCanBeReviewedOnlyByModeratorRule(zoneUser));

            Status = PostStatus.Rejected;

            PostReviews.Add(
                new PostReview(Id,
                    PostStatus.Rejected,
                    zoneUser.UserId,
                    detail));
        }

        public void Break(ZoneUser zoneUser, string detail)
        {
            CheckRule(new PostCanBeReviewedOnlyByModeratorRule(zoneUser));

            Status = PostStatus.BreakRule;

            PostReviews.Add(
                new PostReview(Id,
                    PostStatus.BreakRule,
                    zoneUser.UserId,
                    detail));
        }

        public void RePost(UserId userId)
        {
            CheckRule(new PostCanBeRePostOnlyByAuthorRule(AuthorId, userId));
            CheckRule(new PostCanBeRePostOnlyRejectOrBreakStatusRule(Status));
            Status = PostStatus.RePost;
        }
    }
}