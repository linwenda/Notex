using System.Threading.Tasks;
using Dapr;
using Funzone.PhotoAlbums.Application.IntegrationEvents.EventHandling;
using Funzone.PhotoAlbums.Application.IntegrationEvents.Events;
using Microsoft.AspNetCore.Mvc;

namespace Funzone.PhotoAlbums.Api.IntegrationEvents
{
    public class IntegrationEventController
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
