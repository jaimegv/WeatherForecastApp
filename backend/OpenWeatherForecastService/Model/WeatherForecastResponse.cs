using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Appsfactory.OpenWeatherForecastService.Model
{
    public class WeatherForecastResponse
    {
        [JsonPropertyName("cod")]
        public string Code { get; init; }
        
        [JsonPropertyName("message")]
        public int Message { get; init; }
        
        [JsonPropertyName("cnt")]
        public int Count { get; init; }
        
        [JsonPropertyName("list")]
        public IEnumerable<TimePointWeatherForecastInfo> Items { get; init; }
    }
}
