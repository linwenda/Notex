using System;
using Funzone.Domain.Posts;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;
using NSubstitute;

namespace Funzone.UnitTests.Posts
{
    public class PostTestBase : TestBase
    {
        protected static Post CreatePost()
        {
            var zoneCounter = Substitute.For<IZoneCounter>();

            var zone = Zone.Create(
                zoneCounter,
                new UserId(Guid.NewGuid()),
                "dotnet",
                "dotnet zone", "");

            return zone.AddPost(new UserId(Guid.NewGuid()),
                "C# 9.0",
                "about C# 9.0",
                PostType.Text);
        }
    }
}