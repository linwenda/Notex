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
        public static UserId AdministratorId => new UserId(Guid.Parse("2a555ae4-85f9-4b86-8717-3aaf52c28fe7"));
        
        public static async Task<Guid> CreateZone()
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
            userContext.UserId.Returns(AdministratorId);
            builder.RegisterInstance(userContext).AsImplementedInterfaces();
        }
    }
}