using System;
using System.Collections.Generic;
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
            ZoneMember member,
            string title,
            string content,
            PostType type) : this()
        {
            CheckRule(new PostCanBeCreatedOnlyByZoneMember(member));

            ZoneId = member.ZoneId;
            AuthorId = member.UserId;
            Title = title;
            Content = content;
            Type = type;

            Id = new PostId(Guid.NewGuid());
            PostedTime = Clock.Now;
            Status = PostStatus.WaitingForReview;
        }

        public static Post Create(ZoneMember member, string title, string content,PostType type)
        {
            return new Post(member, title, content, type);
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

        public void Approve(ZoneMember member)
        {
            CheckRule(new PostCanBeReviewedOnlyByModeratorRule(member));

            Status = PostStatus.Approved;

            PostReviews.Add(
                new PostReview(Id,
                    PostStatus.Approved,
                    member.UserId));
        }

        public void Reject(ZoneMember member, string detail)
        {
            CheckRule(new PostCanBeReviewedOnlyByModeratorRule(member));

            Status = PostStatus.Rejected;

            PostReviews.Add(
                new PostReview(Id,
                    PostStatus.Rejected,
                    member.UserId,
                    detail));
        }

        public void Break(ZoneMember member, string detail)
        {
            CheckRule(new PostCanBeReviewedOnlyByModeratorRule(member));

            Status = PostStatus.BreakRule;

            PostReviews.Add(
                new PostReview(Id,
                    PostStatus.BreakRule,
                    member.UserId,
                    detail));
        }

        public void RePost(UserId userId)
        {
            CheckRule(new PostCanBeRePostOnlyByAuthorRule(AuthorId, userId));
            CheckRule(new PostCanBeRePostOnlyRejectOrBreakStatusRule(Status));
            Status = PostStatus.RePost;
        }

        public PostVote Vote(ZoneMember member, VoteType voteType)
        {
            CheckRule(new PostCanBeVotedOnlyZoneMemberRule(member));
            return new PostVote(Id, member.UserId, voteType);
        }
    }
}