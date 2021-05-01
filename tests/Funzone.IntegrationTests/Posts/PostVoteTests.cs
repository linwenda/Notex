using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Funzone.Application.PostVotes.Commands;
using Funzone.Domain.PostVotes;
using Funzone.Domain.SharedKernel;
using MediatR;
using NUnit.Framework;
using Shouldly;

namespace Funzone.IntegrationTests.Posts
{
    using static TestFixture;

    public class PostVoteTests : TestBase
    {
        [Test]
        public async Task ShouldVotePost()
        {
            var postId = await PostTestHelper.CreatePostByExtraMember(Guid.NewGuid());

            await Run<IMediator,IPostVoteRepository>(async (mediator,repository) =>
            {
                var voteId = await mediator.Send(new VotePostCommand
                {
                    PostId = postId,
                    VoteType = VoteType.Up.Value
                });

                var vote = await repository.GetByIdAsync(new PostVoteId(voteId));
                vote.VoteType.ShouldBe(VoteType.Up);
            });
        }

        [Test]
        public async Task ShouldReVotePost()
        {
            var postId = await PostTestHelper.CreatePostByExtraMember(Guid.NewGuid());

            await Run<IMediator, IPostVoteRepository>(async (mediator, repository) =>
            {
                var voteId = await mediator.Send(new VotePostCommand
                {
                    PostId = postId,
                    VoteType = VoteType.Up.Value
                });

                await mediator.Send(new ReVotePostCommand
                {
                    VoteId = voteId,
                    VoteType = VoteType.Neutral.Value
                });

                var vote = await repository.GetByIdAsync(new PostVoteId(voteId));
                vote.VoteType.ShouldBe(VoteType.Neutral);
            });
        }
    }
}