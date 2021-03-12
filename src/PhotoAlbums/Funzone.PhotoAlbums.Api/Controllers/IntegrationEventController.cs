using Dapr;
using Funzone.PhotoAlbums.Application.IntegrationEvents.EventHandling;
using Funzone.PhotoAlbums.Application.IntegrationEvents.Events;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Funzone.PhotoAlbums.Api.Controllers
{
    public class IntegrationEventController
    {
        private const string DaprPubSubName = "pubsub";
        private readonly UserRegisteredIntegrationEventHandler _userRegisteredIntegrationEventHandler;

        public IntegrationEventController(
            UserRegisteredIntegrationEventHandler userRegisteredIntegrationEventHandler)
        {
            _userRegisteredIntegrationEventHandler = userRegisteredIntegrationEventHandler;
        }

        [HttpPost("UserRegistered")]
        [Topic(DaprPubSubName, "UserRegisteredIntegrationEvent")]
        public async Task UserRegistered(UserRegisteredIntegrationEvent @event)
        {
            await _userRegisteredIntegrationEventHandler.Handle(@event);
        }
    }
}
