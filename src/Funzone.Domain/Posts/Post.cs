using System;
using System.Collections.Generic;
using Funzone.Domain.Posts.Events;
using Funzone.Domain.Posts.Rules;
using Funzone.Domain.PostVotes;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneMembers;
using Funzone.Domain.Zones;

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

        public Post(
            ZoneId zoneId,
            UserId authorId,
            string title,
            string content,
            PostType type) : this()
        {
            ZoneId = zoneId;
            AuthorId = authorId;
            Title = title;
            Content = content;
            Type = type;

            Id = new PostId(Guid.NewGuid());
            PostedTime = SystemClock.Now;
            Status = PostStatus.Approved;

            AddDomainEvent(new PostCreatedDomainEvent(Id, AuthorId, Title));
        }

        public void Edit(UserId editorId, string title, string content)
        {
            CheckRule(new PostCanBeEditedOnlyByAuthorRule(AuthorId, editorId));
            Title = title;
            Content = content;
            EditedTime = SystemClock.Now;
        }

        public void Delete(UserId deleterId)
        {
            CheckRule(new PostCanBeDeletedOnlyByAuthorRule(AuthorId, deleterId));
            IsDeleted = true;
        }

        public void Approve(ZoneMember member)
        {
            CheckRule(new PostCanBeApprovedOnlyWaitingOrRePostStatusRule(Status));
            CheckRule(new PostCanBeReviewedOnlyByModeratorRule(member));
            CheckRule(new PostCannotBeReviewedByAuthorRule(AuthorId, member.UserId));

            Status = PostStatus.Approved;

            PostReviews.Add(
                new PostReview(
                    Id,
                    PostStatus.Approved,
                    member.UserId));

            AddDomainEvent(new PostApprovedDomainEvent(Id));
        }

        public void Reject(ZoneMember member, string comment)
        {
            CheckRule(new PostCanBeReviewedOnlyByModeratorRule(member));
            CheckRule(new PostCannotBeReviewedByAuthorRule(AuthorId, member.UserId));

            Status = PostStatus.Rejected;

            PostReviews.Add(
                new PostReview(
                    Id,
                    PostStatus.Rejected,
                    member.UserId,
                    comment));

            AddDomainEvent(new PostRejectedDomainEvent(Id));
        }

        public void RePost(UserId userId)
        {
            CheckRule(new PostCanBeRePostOnlyByAuthorRule(AuthorId, userId));
            CheckRule(new PostCanBeRePostedOnlyRejectStatusRule(Status));
            Status = PostStatus.RePost;
        }

        public PostVote Vote(UserId voterId, VoteType voteType)
        {
            return new PostVote(
                Id,
                voterId,
                voteType);
        }
    }
}