using System;
using Funzone.Domain.Posts;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneMembers;
using Funzone.Domain.Zones;

namespace Funzone.UnitTests.Posts
{
    public class PostTestBase : TestBase
    {
        protected static Post CreateDefaultPost()
        {
            return new Post(
                new ZoneId(Guid.NewGuid()),
                new UserId(Guid.NewGuid()),
                "dotnet",
                "dotnet zone",
                PostType.Text);
        }

        protected class PostTestDataOptions
        {
            public Post Post { get; set; }
            public ZoneMember Member { get; set; }
            public ZoneMember Moderator { get; set; }
        }

        protected class PostTestData
        {
            public PostTestData(Post post, ZoneMember member, ZoneMember moderator)
            {
                Post = post;
                Member = member;
                Moderator = moderator;
            }

            public Post Post { get; }
            public ZoneMember Member { get; }
            public ZoneMember Moderator { get; }
        }

        protected static PostTestData CreatePostTestData(PostTestDataOptions options)
        {
            var post = options.Post ?? CreateDefaultPost();

            var member = options.Member ??
                         new ZoneMember(
                             post.ZoneId,
                             new UserId(Guid.NewGuid()),
                             ZoneMemberRole.Member);

            var moderator = options.Moderator ??
                            new ZoneMember(
                                post.ZoneId,
                                new UserId(Guid.NewGuid()),
                                ZoneMemberRole.Moderator);

            return new PostTestData(post, member, moderator);
        }
    }
}