using Appsfactory.OpenWeatherForecastService.Services;
using Appsfactory.OpenWeatherForecastService.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Text.Json;
using Appsfactory.WeatherForecast.SharedTypes.Configuration;

namespace Appsfactory.OpenWeatherForecastService.Services
{
    public class OpenWeatherForecastHttpClient : IWeatherForecastDataProviderClient
    {
        private readonly ILogger<OpenWeatherForecastHttpClient> _logger;
        private readonly ExternalWeatherApiConfiguration _weatherServiceConfiguration;
        private readonly HttpClient _httpClient;

        public OpenWeatherForecastHttpClient(
            ILogger<OpenWeatherForecastHttpClient> logger,
            ExternalWeatherApiConfiguration weatherServiceConfiguration,
            HttpClient httpClient)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _weatherServiceConfiguration = weatherServiceConfiguration ?? throw new ArgumentNullException(nameof(weatherServiceConfiguration));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<WeatherForecastResponse> GetRawWeatherForecastDataByCity(string city)
        {
            _logger.LogDebug($"{nameof(GetRawWeatherForecastDataByCity)} - {city} called");
            var query = GetBaseQueryString();
            query["q"] = $"{city}{GetCountryFlagIfExist()}";
            var jsonData = await _httpClient.GetStringAsync($"?{query.ToString()}");
            return ConvertJsonToWeatherForecastData(jsonData);
        }

        public async Task<WeatherForecastResponse> GetRawWeatherForecastDataByZipCode(string zipCode)
        {
            _logger.LogDebug($"{nameof(GetRawWeatherForecastDataByZipCode)} - {zipCode} called");
            var query = GetBaseQueryString();
            query["zip"] = $"{zipCode}{GetCountryFlagIfExist()}";
            var jsonData = await _httpClient.GetStringAsync($"?{query.ToString()}");
            return ConvertJsonToWeatherForecastData(jsonData);
        }

        private string GetCountryFlagIfExist() =>
            string.IsNullOrEmpty(_weatherServiceConfiguration.CountryCode) ?
                "" : $",{_weatherServiceConfiguration.CountryCode}";

        private System.Collections.Specialized.NameValueCollection GetBaseQueryString()
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["units"] = _weatherServiceConfiguration.Units;
            query["appid"] = _weatherServiceConfiguration.ApiKey;
            return query;
        }

        private WeatherForecastResponse ConvertJsonToWeatherForecastData(string jsonData)
        {
            if(string.IsNullOrEmpty(jsonData))
            {
                return null;
            }
            return JsonSerializer.Deserialize<WeatherForecastResponse>(jsonData);
        }
    }
}
