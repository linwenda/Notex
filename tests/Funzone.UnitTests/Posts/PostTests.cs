using System;
using Funzone.Domain.Posts;
using Funzone.Domain.Posts.Rules;
using Funzone.Domain.Users;
using NUnit.Framework;

namespace Funzone.UnitTests.Posts
{
    public class PostTests : PostTestBase
    {
        [Test]
        public void EditPost_ByNoAuthor_BreakPostCanBeEditedOnlyByAuthorRule()
        {
            var post = CreatePost();

            ShouldBrokenRule<PostCanBeEditedOnlyByAuthorRule>(()=>
                post.Edit(new UserId(Guid.NewGuid()), "Title", "Content"));
        }

        [Test]
        public void EditPost_ByAuthor_Successful()
        {
            var post = CreatePost();
        }
    }
}