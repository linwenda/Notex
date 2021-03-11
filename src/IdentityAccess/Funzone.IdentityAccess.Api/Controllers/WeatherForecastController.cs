using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Funzone.IdentityAccess.Application.IntegrationEvents.Events;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr.Client;

namespace Funzone.IdentityAccess.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IEventBus _eventBus;

        public WeatherForecastController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            await _eventBus.Publish(
                new UserRegisteredIntegrationEvent(Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(),
                "test", "test@email"));

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = "test"
                })
                .ToArray();
        }
    }
}