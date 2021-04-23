using System;
using System.Threading.Tasks;
using Funzone.Application.Commands.Zones;
using MediatR;

namespace Funzone.IntegrationTests.Zones
{
    using static TestFixture;

    public class ZoneTestHelper
    {
        public static async Task<Guid> CreateZone()
        {
            return await Run<IMediator, Guid>(async mediator =>
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
    }
}