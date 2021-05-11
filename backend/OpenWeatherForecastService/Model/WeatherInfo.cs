using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Appsfactory.OpenWeatherForecastService.Model
{
    [DebuggerDisplay("{Temperature}.{MaxTemperature}.{MinTemperature}.{Humidity}")]
    public class WeatherInfo
    {
        [JsonPropertyName("temp")]
        public double Temperature { get; init; }
        
        [JsonPropertyName("temp_max")]
        public double MaxTemperature { get; init; }
        
        [JsonPropertyName("temp_min")]
        public double MinTemperature { get; init; }
        
        [JsonPropertyName("humidity")]
        public double Humidity { get; init; }
    }
}