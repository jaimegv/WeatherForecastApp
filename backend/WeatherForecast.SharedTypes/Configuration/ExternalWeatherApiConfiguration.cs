namespace Appsfactory.WeatherForecast.SharedTypes.Configuration
{
    public class ExternalWeatherApiConfiguration
    {
        public string ApiKey { get; init; }
        public string Url { get; init; }
        public string Units { get; init; }
        public string CountryCode { get; init; }
    }
}
