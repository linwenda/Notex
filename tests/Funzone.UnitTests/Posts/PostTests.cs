using System;
using System.Linq;
using Funzone.Domain.Posts;
using Funzone.Domain.Posts.Rules;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneMembers;
using NUnit.Framework;
using Shouldly;

namespace Funzone.UnitTests.Posts
{
    public class PostTests : PostTestBase
    {
        [Test]
        public void EditPost_ByNoAuthor_BreakPostCanBeEditedOnlyByAuthorRule()
        {
            var post = CreateDefaultPost();

            ShouldBrokenRule<PostCanBeEditedOnlyByAuthorRule>(() =>
                post.Edit(new UserId(Guid.NewGuid()), "Title", "Content"));
        }

        [Test]
        public void EditPost_ByAuthor_Successful()
        {
            var post = CreateDefaultPost();

            var editedTime = new DateTime(2020, 1, 2, 1, 0, 0);

            Clock.Set(editedTime);

            var editInfo = new
            {
                Title = "Testing",
                Content = "text"
            };

            post.Edit(post.AuthorId, editInfo.Title, editInfo.Content);
            post.Title.ShouldBe(editInfo.Title);
            post.Content.ShouldBe(editInfo.Content);
            post.EditedTime.ShouldBe(editedTime);

            Clock.Reset();
        }

        [Test]
        public void DeletePost_ByNoAuthor_BreakPostCanBeDeletedOnlyByAuthorRule()
        {
            var post = CreateDefaultPost();

            ShouldBrokenRule<PostCanBeDeletedOnlyByAuthorRule>(() =>
                post.Delete(new UserId(Guid.NewGuid())));
        }

        [Test]
        public void Delete_ByAuthor_Successful()
        {
            var post = CreateDefaultPost();
            post.Delete(post.AuthorId);
            post.IsDeleted.ShouldBe(true);
        }

        [Test]
        public void Reject_WithMember_BreakPostCanBeReviewedOnlyByModeratorRule()
        {
            var testData = CreatePostTestData(new PostTestDataOptions());

            ShouldBrokenRule<PostCanBeReviewedOnlyByModeratorRule>(() =>
                testData.Post.Reject(testData.Member, "reject"));
        }

        [Test]
        public void Reject_ByAuthor_BreakPostCannotBeReviewedByAuthorRule()
        {
            var post = CreateDefaultPost();

            var testData = CreatePostTestData(new PostTestDataOptions
            {
                Post = post,
                Moderator = new ZoneMember(post.ZoneId, post.AuthorId, ZoneMemberRole.Moderator)
            });

            ShouldBrokenRule<PostCannotBeReviewedByAuthorRule>(() =>
                testData.Post.Reject(testData.Moderator, "reject"));
        }

        [Test]
        public void Reject_WithModerator_Successful()
        {
            var testData = CreatePostTestData(new PostTestDataOptions());

            testData.Post.Reject(testData.Moderator, "reject");

            testData.Post.Status.ShouldBe(PostStatus.Rejected);
            ShouldAddPostReview(testData.Post, testData.Moderator.UserId, PostStatus.Rejected, "reject");
        }

        [Test]
        public void RePost_AfterRejected_Successful()
        {
            var testData = CreatePostTestData(new PostTestDataOptions());

            testData.Post.Reject(testData.Moderator, "reject");
            testData.Post.RePost(testData.Post.AuthorId);
            testData.Post.Status.ShouldBe(PostStatus.RePost);
        }

        [Test]
        public void RePost_ByNoAuthor_BreakPostCanBeRePostOnlyByAuthorRule()
        {
            var testData = CreatePostTestData(new PostTestDataOptions());

            testData.Post.Reject(testData.Moderator, "reject");

            ShouldBrokenRule<PostCanBeRePostOnlyByAuthorRule>(() =>
                testData.Post.RePost(new UserId(Guid.NewGuid())));
        }

        [Test]
        public void RePost_AfterApproved_BreakPostCanBeRePostedOnlyRejectStatusRule()
        {
            var testData = CreatePostTestData(new PostTestDataOptions());

            testData.Post.Reject(testData.Moderator, "reject");
            testData.Post.RePost(testData.Post.AuthorId);
            testData.Post.Approve(testData.Moderator);

            ShouldBrokenRule<PostCanBeRePostedOnlyRejectStatusRule>(() =>
                testData.Post.RePost(testData.Post.AuthorId));
        }

        [Test]
        public void Approve_AfterRejected_BreakPostCanBeApprovedOnlyWaitingOrRePostStatusRule()
        {
            var testData = CreatePostTestData(new PostTestDataOptions());

            testData.Post.Reject(testData.Moderator, "reject");

            ShouldBrokenRule<PostCanBeApprovedOnlyWaitingOrRePostStatusRule>(() =>
                testData.Post.Approve(testData.Moderator));
        }

        [Test]
        public void Approve_AfterRePosted_Successful()
        {
             var testData = CreatePostTestData(new PostTestDataOptions());

            testData.Post.Reject(testData.Moderator, "reject");
            testData.Post.RePost(testData.Post.AuthorId);
            testData.Post.Approve(testData.Moderator);
            testData.Post.Status.ShouldBe(PostStatus.Approved);

            ShouldAddPostReview(testData.Post, testData.Moderator.UserId, PostStatus.Approved, "");
        }

        private static void ShouldAddPostReview(Post post, UserId reviewerId, PostStatus reviewStatus, string comment)
        {
            var postReview = post.PostReviews.OrderBy(pr => pr.ReviewedTime).LastOrDefault();
            postReview.ShouldNotBeNull();
            postReview.PostStatus.ShouldBe(reviewStatus);
            postReview.ReviewerId.ShouldBe(reviewerId);
            postReview.Comment.ShouldBe(comment);
        }
    }
}