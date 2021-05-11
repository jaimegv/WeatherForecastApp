using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Appsfactory.OpenWeatherForecastService.Model
{
    [DebuggerDisplay("{Speed}")]
    public class WindInfo
    {
        [JsonPropertyName("speed")]
        public double Speed { get; init; }
    }
}