using Dapr;
using Funzone.Services.Albums.Application.IntegrationEvents.EventHandling;
using Funzone.Services.Albums.Application.IntegrationEvents.Events;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Funzone.Services.Albums.Api.Controllers
{
    [ApiController]
    public class IntegrationEventController : ControllerBase
    {
        private const string DaprPubSub = "pubsub";
        private readonly UserRegisteredIntegrationEventHandler _userRegisteredIntegrationEventHandler;

        public IntegrationEventController(
            UserRegisteredIntegrationEventHandler userRegisteredIntegrationEventHandler)
        {
            _userRegisteredIntegrationEventHandler = userRegisteredIntegrationEventHandler;
        }

        [HttpPost("UserRegistered")]
        [Topic(DaprPubSub, "UserRegisteredIntegrationEvent")]
        public async Task UserRegistered(UserRegisteredIntegrationEvent @event)
        {
            await _userRegisteredIntegrationEventHandler.Handle(@event);
        }
    }
}