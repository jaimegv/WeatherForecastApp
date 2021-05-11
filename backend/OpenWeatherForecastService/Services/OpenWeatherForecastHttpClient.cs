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

        public async Task<WeatherForecastResponse> GetJsonWeatherForecastDataByCity(string city)
        {
            var query = GetBaseQueryString();
            query["q"] = $"{city},{_weatherServiceConfiguration.CountryCode}";
            //query["q"] = city;
            var jsonData = await _httpClient.GetStringAsync($"?{query.ToString()}");
            return ConvertJsonToWeatherForecastData(jsonData);
        }

        public async Task<WeatherForecastResponse> GetJsonWeatherForecastDataByZipCode(string zipCode)
        {
            var query = GetBaseQueryString();
            query["zip"] = $"{zipCode},{_weatherServiceConfiguration.CountryCode}";
            string queryString = query.ToString();
            var jsonData = await _httpClient.GetStringAsync($"?{query.ToString()}");

            return ConvertJsonToWeatherForecastData(jsonData);
        }

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
