using System;
using System.Threading.Tasks;
using Autofac;
using Funzone.Application.Posts.Commands;
using Funzone.Domain.Posts;
using Funzone.IntegrationTests.Zones;
using MediatR;

namespace Funzone.IntegrationTests.Posts
{
    using static TestFixture;

    public static class PostTestHelper
    {
        public static async Task<Guid> CreatePostByMember()
        {
            var zoneId = await ZoneTestHelper.CreateZoneWithExtraUserAsync();

            return await Run<IMediator, Guid>(
                async mediator =>
                {
                    var addPostCommand = new AddPostCommand
                    {
                        Title = ".NET Core Overview",
                        Content = ".NET Core is a new version of .NET Framework",
                        Type = PostType.Text.Value,
                        ZoneId = zoneId
                    };

                    return await mediator.Send(addPostCommand);
                });
        }

        public static async Task<Guid> CreatePostByExtraMember(Guid currentUserId)
        {
            var zoneId = await ZoneTestHelper.CreateZoneAsync();

            return await RunAsRegisterExtra<IMediator, Guid>(
                async mediator =>
                {
                    var addPostCommand = new AddPostCommand
                    {
                        Title = ".NET Core Overview",
                        Content = ".NET Core is a new version of .NET Framework",
                        Type = PostType.Text.Value,
                        ZoneId = zoneId
                    };

                    return await mediator.Send(addPostCommand);
                },
                registerExtra => ReRegisterUserContext(registerExtra, currentUserId));
        }
    }
}