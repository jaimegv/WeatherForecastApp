using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Appsfactory.WeatherForecast.SharedTypes.Contracts;
using Appsfactory.WeatherForecast.SharedTypes.UIDto;
using Appsfactory.OpenWeatherForecastService.Model;

namespace Appsfactory.OpenWeatherForecastService.Services
{
    public class WeatherForecastDataProvider : ICanGetWeatherForecastData
    {
        private const string ResponseOkCode = "200";
        private readonly ILogger<WeatherForecastDataProvider> _logger;
        private readonly IWeatherForecastDataProviderClient _weatherForecastDataProviderClient;

        public WeatherForecastDataProvider(
            ILogger<WeatherForecastDataProvider> logger,
            IWeatherForecastDataProviderClient weatherForecastDataProviderClient)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _weatherForecastDataProviderClient = weatherForecastDataProviderClient ?? throw new ArgumentNullException(nameof(weatherForecastDataProviderClient));
        }

        public async Task<IEnumerable<WeatherForecastDto>> GetWeatherForecastDataByCity(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                _logger.LogError($"City param is null or empty {nameof(city)}");
            }
            return await GetWeatherForecastData(() => _weatherForecastDataProviderClient.GetJsonWeatherForecastDataByCity(city));
        }


        public async Task<IEnumerable<WeatherForecastDto>> GetWeatherForecastDataByZipCode(string zipCode)
        {
            if (string.IsNullOrEmpty(zipCode))
            {
                _logger.LogError($"ZipCode param is null or empty {nameof(zipCode)}");
            }
            return await GetWeatherForecastData(() => _weatherForecastDataProviderClient.GetJsonWeatherForecastDataByZipCode(zipCode));
        }

        private IEnumerable<WeatherForecastDto> ConvertToDailyWeatherForecastData(WeatherForecastResponse weatherForecastResponse)
        {
            return weatherForecastResponse.Items
                .GroupBy(i => i.ForecastDateTime.Date)
                .Select(i => new WeatherForecastDto
                { 
                    Date = i.Key.Date.ToUniversalTime(),
                    Temperature = Math.Round(i.Average(x => x.Info.Temperature), 2), 
                    Humidity = Math.Round(i.Average(x => x.Info.Humidity), 0),
                    WindSpeed = Math.Round(i.Average(x => x.WindInfo.Speed), 2)
                });
        }

        private async Task<IEnumerable<WeatherForecastDto>> GetWeatherForecastData(Func<Task<WeatherForecastResponse>> getWeatherForecastDataFromSource)
        {
            var result = Enumerable.Empty<WeatherForecastDto>();
            try
            {
                var weatherForecastData = await getWeatherForecastDataFromSource();
                if (weatherForecastData is not null && weatherForecastData.Code == ResponseOkCode)
                {
                    result = ConvertToDailyWeatherForecastData(weatherForecastData);
                }
                else
                {
                    _logger.LogError("");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
                throw;
            }
            return result;
        }

    }
}
