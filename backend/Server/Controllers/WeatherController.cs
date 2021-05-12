using Appsfactory.WeatherForecast.SharedTypes.Contracts;
using Appsfactory.WeatherForecast.SharedTypes.UIDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public async Task<ActionResult<IEnumerable<WeatherForecastDto>>> GetWeatherForecast(string city, string zipCode)
        {
            _logger.LogDebug($"{nameof(GetWeatherForecast)} called, city: {city}, zipCode: {zipCode}");

            if (!string.IsNullOrEmpty(city))
            {
                return await GetWeatherForecastData(city, (c) => _weatherForecastDataProvider.GetWeatherForecastDataByCity(c));
            }
            else if (!string.IsNullOrEmpty(zipCode))
            {
                return await GetWeatherForecastData(city, (c) => _weatherForecastDataProvider.GetWeatherForecastDataByZipCode(zipCode));
            }
            else
            {
                _logger.LogError("Wrong input values");
                return BadRequest("Wrong input values");
            }
        }

        private async Task<ActionResult<IEnumerable<WeatherForecastDto>>> GetWeatherForecastData(
            string value, Func<string, Task<IEnumerable<WeatherForecastDto>>> weatherForecastProvider)
        {
            var result = Enumerable.Empty<WeatherForecastDto>();
            try
            {
                result = await weatherForecastProvider(value);
                if (result is null)
                {
                    _logger.LogError("Weather forecast provider returned null");
                    return BadRequest();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"HttpRequestException - {ex.StatusCode}");
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected Error");
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }
    }
}
