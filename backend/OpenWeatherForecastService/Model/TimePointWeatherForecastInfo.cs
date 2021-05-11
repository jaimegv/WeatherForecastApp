using Appsfactory.WeatherForecast.SharedTypes.JsonConverters;
using System;
using System.Text.Json.Serialization;

namespace Appsfactory.OpenWeatherForecastService.Model
{
    public class TimePointWeatherForecastInfo
    {
        [JsonPropertyName("dt")]
        public long ForecastTimeStamp { get; init; }

        [JsonPropertyName("main")]
        public WeatherInfo Info { get; init; }

        [JsonPropertyName("dt_txt")]
        [JsonConverter(typeof(JsonDateTimeConverter))]
        public DateTime ForecastDateTime { get; init; }
        
        [JsonPropertyName("wind")]
        public WindInfo WindInfo { get; init; }
        
    }
}