using Dapr.Client;
using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Serilog;
using System.Threading.Tasks;

namespace Funzone.BuildingBlocks.EventBusDapr
{
    /// <summary>
    /// Based on https://github.com/dotnet-architecture/eShopOnDapr
    /// </summary>
    public class DaprEventBus : IEventBus
    {
        private const string DaprPubsubName = "pubsub";

        private readonly DaprClient _dapr;
        private readonly ILogger _logger;

        public DaprEventBus(DaprClient dapr, ILogger logger)
        {
            _dapr = dapr;
            _logger = logger;
        }

        public async Task Publish<TIntegrationEvent>(TIntegrationEvent @event)
            where TIntegrationEvent : IntegrationEvent
        {
            var topicName = @event.GetType().Name;

            _logger.Information("Publishing event {@Event} to {PubsubName}.{TopicName}",
                @event, 
                DaprPubsubName,
                topicName);

            // We need to make sure that we pass the concrete type to PublishEventAsync,
            // which can be accomplished by casting the event to dynamic. This ensures
            // that all event fields are properly serialized.
            await _dapr.PublishEventAsync(DaprPubsubName, topicName, (dynamic) @event);
        }
    }
}