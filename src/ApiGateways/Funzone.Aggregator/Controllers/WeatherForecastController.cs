using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Funzone.Aggregator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly DaprClient _daprClient;

        public WeatherForecastController(DaprClient daprClient)
        {
            _daprClient = daprClient ?? throw new ArgumentNullException(nameof(daprClient));
        }


        [Route("identityaccess")]
        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await _daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(
                HttpMethod.Get,
                "identityaccessapi",
                "weatherforecast");
        }

        [Route("photoAlbum")]
        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> PhotoAlbum()
        {
            return await _daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(
                HttpMethod.Get,
                "photoalbumsapi",
                "weatherforecast");
        }
    }
}
