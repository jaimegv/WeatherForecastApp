using Appsfactory.OpenWeatherForecastService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appsfactory.OpenWeatherForecastService.Services
{
    /// <summary>
    /// This interface provides weather forecast data provided by OpenWeatherForecast
    /// for the next 5 days
    /// </summary>
    public interface IWeatherForecastDataProviderClient
    {
        /// <summary>
        /// Gets the weather forecast data defined by the given city name as Json<paramref name="city"/>
        /// </summary>
        /// <param name="city">City name</param>
        /// <returns>Returns the weather forecast raw data</returns>
        Task<WeatherForecastResponse> GetRawWeatherForecastDataByCity(string city);


        /// <summary>
        /// Gets the weather forecast data defined by the given zip code as Json<paramref name="zipCode"/>
        /// </summary>
        /// <param name="zipCode">Zip code</param>
        /// <returns>Returns the weather forecast raw data</returns>
        Task<WeatherForecastResponse> GetRawWeatherForecastDataByZipCode(string zipCode);
    }
}
