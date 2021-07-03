using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.API.Models.Dto;
using Weather.API.Processors;

namespace Weather.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherProcessors _weatherProcessors;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherProcessors weatherProcessors)
        {
            _logger = logger;
            _weatherProcessors = weatherProcessors;
        }

        [HttpGet("GetByCity")]
        public async Task<ActionResult> GetByCityAsync(string cityName)
        {
            var response = await _weatherProcessors.ProcessAsync($"forecast?q={cityName}");
            if (response != null)
            {
                return Ok(response);
            }
            return StatusCode(404, new ErrorResponse() { ErrorCode = "500", ErrorMessage = "openweathermap does not work currently." });
        }

        [HttpGet("GetByZipCode")]
        public async Task<ActionResult> GetByZipCodeAsync(string zipCode)
        {
            var response = await _weatherProcessors.ProcessAsync($"forecast?zip={zipCode}");
            if (response != null)
            {
                return Ok(response);
            }
            return StatusCode(404, new ErrorResponse() { ErrorCode = "500", ErrorMessage = $"openweathermap does not work currently." });
        }
    }
}
