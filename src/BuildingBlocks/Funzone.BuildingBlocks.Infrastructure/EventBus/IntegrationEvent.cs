using System;
using Newtonsoft.Json;

namespace Funzone.BuildingBlocks.Infrastructure.EventBus
{
    public class IntegrationEvent
    {
        public Guid Id { get; private set; }

        public DateTime OccurredOn { get; private set; }

        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            OccurredOn = DateTime.UtcNow;
        }
    }
}
