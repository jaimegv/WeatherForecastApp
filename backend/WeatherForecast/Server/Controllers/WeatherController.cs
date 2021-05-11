using Appsfactory.WeatherForecast.SharedTypes.Contracts;
using Appsfactory.WeatherForecast.SharedTypes.UIDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appsfactory.WeatherForecast.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/Forecast")]
    public class WeatherController : Controller
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly ICanGetWeatherForecastData _weatherForecastDataProvider;

        public WeatherController(
            ILogger<WeatherController> logger,
            ICanGetWeatherForecastData weatherForecastDataProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _weatherForecastDataProvider = weatherForecastDataProvider ?? throw new ArgumentNullException(nameof(weatherForecastDataProvider));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecastDto>>> Get(string city, string zipCode)
        {
            _logger.LogDebug($"{nameof(Get)} called, city: {city}, zipCode: {zipCode}");
            var result = Enumerable.Empty<WeatherForecastDto>();
            if (!string.IsNullOrEmpty(city))
            {
                try
                {
                    result = await _weatherForecastDataProvider.GetWeatherForecastDataByCity(city);
                    if (result is null)
                    {
                        _logger.LogError("");
                        return BadRequest();
                    }
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "");
                    return BadRequest(ex.Message);
                }
            }
            else if (!string.IsNullOrEmpty(zipCode))
            {
                try
                {
                    result = await _weatherForecastDataProvider.GetWeatherForecastDataByZipCode(zipCode);
                    if(result is null)
                    {
                        _logger.LogError("");
                        return BadRequest();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "");
                    return BadRequest(ex.Message);
                }
            }
            return Ok(result);
        }
    }
}
