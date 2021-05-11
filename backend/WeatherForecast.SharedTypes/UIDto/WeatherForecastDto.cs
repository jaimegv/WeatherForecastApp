using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Appsfactory.WeatherForecast.SharedTypes.UIDto
{
    [DebuggerDisplay("{Date}.{Temperature} ºC - {Humidity}, Wind speed: {WindSpeed}")]
    public record WeatherForecastDto
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; init; }

        [JsonPropertyName("temperature")]
        public double Temperature { get; init; }
        
        [JsonPropertyName("windspeed")]
        public double WindSpeed { get; init; }
        
        [JsonPropertyName("humidity")]
        public double Humidity { get; init; }
    }
}
