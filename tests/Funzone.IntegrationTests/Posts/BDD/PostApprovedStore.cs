using Funzone.Application.Posts.Commands;
using Funzone.Domain.Posts;
using Funzone.Domain.Users;
using MediatR;
using NUnit.Framework;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestStack.BDDfy;

namespace Funzone.IntegrationTests.Posts.BDD
{
    using static TestFixture;

    [Story]
    public class PostApprovedStore : TestBase
    {
        private Guid _currentUserId;
        private Guid _postId;

        private async Task GivenNewPost()
        {
            _currentUserId = Guid.NewGuid();
            _postId = await PostTestHelper.CreatePostByExtraMember(_currentUserId);
        }

        private async Task WhenAdministratorRejectPost()
        {
            //Reject by zone administrator
            await Run<IMediator>(async mediator =>
            {
                await mediator.Send(new RejectPostCommand
                {
                    PostId = _postId,
                    Reason = "Reject"
                });
            });
        }

        private async Task ThenAuthorRePost()
        {
            await RunAsRegisterExtra<IMediator, IPostRepository>(
                async (mediator, repository) =>
                {
                    await mediator.Send(new RePostCommand
                    {
                        PostId = _postId
                    });

                    var post = await repository.GetByIdAsync(new PostId(_postId));
                    post.Status.ShouldBe(PostStatus.RePost);
                },
                builder => ReRegisterUserContext(builder, _currentUserId));
        }

        private async Task ThenAdministratorApproved()
        {
            //Approve by zone administrator
            await Run<IMediator>(async mediator =>
            {
                await mediator.Send(new ApprovePostCommand
                {
                    PostId = _postId
                });
            });
        }

        private async Task AndPostStatusShouldBeApproved()
        {
            await Run<IPostRepository>(async repository =>
            {
                var post = await repository.GetByIdAsync(new PostId(_postId));
                post.Status.ShouldBe(PostStatus.Approved);

                var postReview = post.PostReviews.OrderBy(p => p.ReviewedTime).Last();
                postReview.ReviewerId.ShouldBe(new UserId(CurrentUserId)); //Administrator
                postReview.PostStatus.ShouldBe(PostStatus.Approved);
            });
        }

        [Test]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}