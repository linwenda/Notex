using System;
using System.Threading.Tasks;
using Autofac;
using Funzone.Application.Commands.Zones;
using Funzone.Domain.Users;
using MediatR;
using NSubstitute;

namespace Funzone.IntegrationTests.Zones
{
    using static TestFixture;

    public class ZoneTestHelper
    {
        public static async Task<Guid> CreateZoneAsync()
        {
            return await Run<IMediator, Guid>(
                async mediator =>
                {
                    var command = new CreateZoneCommand
                    {
                        Title = "LOL",
                        Description = "League of legends",
                        AvatarUrl = "https://image.service.com"
                    };

                    return await mediator.Send(command);
                });
        }

        public static async Task<Guid> CreateZoneWithOtherUserAsync()
        {
            return await RunAsRegisterExtra<IMediator, Guid>(
                ReRegisterUserContext,
                async mediator =>
            {
                var command = new CreateZoneCommand
                {
                    Title = "LOL",
                    Description = "League of legends",
                    AvatarUrl = "https://image.service.com"
                };

                return await mediator.Send(command);
            });
        }
        
        private static void ReRegisterUserContext(ContainerBuilder builder)
        {
            var userContext = Substitute.For<IUserContext>();
            userContext.UserId.Returns(new UserId(Guid.NewGuid()));
            builder.RegisterInstance(userContext).AsImplementedInterfaces();
        }
    }
}