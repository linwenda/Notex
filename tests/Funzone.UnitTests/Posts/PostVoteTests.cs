using System;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;
using NUnit.Framework;

namespace Funzone.UnitTests.Posts
{
    public class PostVoteTests : PostTestBase
    {
        [Test]
        public void Vote_Post_WithSuccessful()
        {
            var post = CreateDefaultPost();

            post.Vote(new UserId(Guid.NewGuid()), VoteType.Up);
        }
    }
}