using Appsfactory.WeatherForecast.SharedTypes.UIDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appsfactory.WeatherForecast.SharedTypes.Contracts
{
    /// <summary>
    /// This interface provides weather forecast data for the next 5 days
    /// </summary>
    public interface ICanGetWeatherForecastData
    {
        /// <summary>
        /// Gets the weather forecast data defined by the given city name <paramref name="city"/>
        /// </summary>
        /// <param name="city">City name</param>
        /// <returns>Returns the weather forecast data</returns>
        Task<IEnumerable<WeatherForecastDto>> GetWeatherForecastDataByCity(string city);


        /// <summary>
        /// Gets the weather forecast data defined by the given zip code <paramref name="zipCode"/>
        /// </summary>
        /// <param name="zipCode">Zip code</param>
        /// <returns>Returns the weather forecast data</returns>
        Task<IEnumerable<WeatherForecastDto>> GetWeatherForecastDataByZipCode(string zipCode);
    }
}
