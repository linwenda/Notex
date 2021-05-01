using System;
using System.Linq;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Exceptions;
using Funzone.Application.Posts.Commands;
using Funzone.Domain.Posts;
using Funzone.Domain.Zones;
using Funzone.IntegrationTests.Zones;
using MediatR;
using NUnit.Framework;
using Shouldly;

namespace Funzone.IntegrationTests.Posts
{
    using static TestFixture;

    public class PostTests : TestBase
    {
        [Test]
        public async Task ShouldAddPost()
        {
            var zoneId = await ZoneTestHelper.CreateZoneAsync();

            await Run<IMediator, IPostRepository>(async (mediator, repository) =>
            {
                var command = new AddPostCommand
                {
                    Title = ".NET Core Overview",
                    Content = ".NET Core is a new version of .NET Framework",
                    Type = PostType.Text.Value,
                    ZoneId = zoneId
                };

                var postId = await mediator.Send(command);

                var post = await repository.GetByIdAsync(new PostId(postId));

                post.ZoneId.ShouldBe(new ZoneId(command.ZoneId));
                post.Title.ShouldBe(command.Title);
                post.Content.ShouldBe(command.Content);
                post.Status.ShouldBe(PostStatus.Approved);
                post.Type.ShouldBe(PostType.Text);
                post.EditedTime.ShouldBeNull();
            });
        }

        [Test]
        public async Task ShouldEditPost()
        {
            var postId = await PostTestHelper.CreatePostByMember();

            await Run<IMediator, IPostRepository>(async (mediator, repository) =>
            {
                var command = new EditPostCommand
                {
                    Title = "Clean Architecture",
                    Content = "Over the last several years we’ve seen a whole range of ideas regarding the architecture of systems",
                    PostId = postId
                };

                await mediator.Send(command);

                var post = await repository.GetByIdAsync(new PostId(postId));

                post.Title.ShouldBe(command.Title);
                post.Content.ShouldBe(command.Content);
                post.EditedTime.ShouldNotBeNull();
            });
        }

        [Test]
        public async Task ShouldDeletePost()
        {
            var postId = await PostTestHelper.CreatePostByMember();

            await Run<IMediator, IPostRepository>(async (mediator, repository) =>
            {
                await mediator.Send(new DeletePostCommand(postId));

                await Should.ThrowAsync<NotFoundException>(async () =>
                    await repository.GetByIdAsync(new PostId(postId)));
            });
        }

        [Test]
        public async Task ShouldRejectPost()
        {
            var postId = await PostTestHelper.CreatePostByExtraMember(Guid.NewGuid());

            await Run<IMediator, IPostRepository>(async (mediator, repository) =>
            {
                await mediator.Send(new RejectPostCommand
                {
                    PostId = postId,
                    Reason = "Reject"
                });

                var post = await repository.GetByIdAsync(new PostId(postId));
                post.Status.ShouldBe(PostStatus.Rejected);

                var pr = post.PostReviews.Single(p => p.PostStatus == PostStatus.Rejected);
                pr.Comment.ShouldBe("Reject");
            });
        }

        [Test]
        public async Task ShouldRePostAfterRejected()
        {
            var currentUserId = Guid.NewGuid();
            var postId = await PostTestHelper.CreatePostByExtraMember(currentUserId);

            //Reject by zone administrator
            await Run<IMediator>(async mediator =>
            {
                await mediator.Send(new RejectPostCommand
                {
                    PostId = postId,
                    Reason = "Reject"
                });
            });

            //RePost by current user
            await RunAsRegisterExtra<IMediator,IPostRepository>(
                async (mediator, repository) =>
                {
                    await mediator.Send(new RePostCommand
                    {
                        PostId = postId
                    });

                    var post = await repository.GetByIdAsync(new PostId(postId));
                    post.Status.ShouldBe(PostStatus.RePost);
                },
                builder => ReRegisterUserContext(builder, currentUserId));
        }
    }
}