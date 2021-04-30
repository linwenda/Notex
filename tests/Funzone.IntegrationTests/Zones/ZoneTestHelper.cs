using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Funzone.Application.Commands.ZoneRules;
using Funzone.Application.Commands.Zones;
using Funzone.Application.Queries.ZoneRules;
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
        
        public static async Task<ZoneRuleDto> CreateZoneRule(Guid zoneId)
        {
            return await Run<IMediator, ZoneRuleDto>(async mediator =>
            {
                var command = new AddZoneRuleCommand
                {
                    ZoneId = zoneId,
                    Title = "No job postings",
                    Description = "Hire or Hiring",
                    Sort = 1
                };

                await mediator.Send(command);

                var zoneRules = await mediator.Send(new GetZoneRulesQuery(zoneId));
                return zoneRules.First();
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