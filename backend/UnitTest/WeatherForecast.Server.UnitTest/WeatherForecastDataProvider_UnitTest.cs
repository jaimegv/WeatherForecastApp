using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Appsfactory.OpenWeatherForecastService.Services;
using Appsfactory.OpenWeatherForecastService.Model;

namespace Appsfactory.WeatherForecast.Server.UnitTest
{
    public class WeatherForecastDataProvider_UnitTest
    {
        [Fact]
        public void Constructor_MockParameters_ShouldNotThrow()
        {
            //arrange
            var loggerMock = new Mock<ILogger<WeatherForecastDataProvider>>();
            var getIWeatherForecastDataProviderClientMock = new Mock<IWeatherForecastDataProviderClient>();

            //act
            Action act = () => new WeatherForecastDataProvider(loggerMock.Object, getIWeatherForecastDataProviderClientMock.Object);

            //assert
            act.Should().NotThrow();
        }

        [Fact]
        public void Constructor_NullParameters_ShouldThrow()
        {
            //arrange
            var loggerMock = new Mock<ILogger<WeatherForecastDataProvider>>();
            var getIWeatherForecastDataProviderClientMock = new Mock<IWeatherForecastDataProviderClient>();
            //act
            Action act1 = () => new WeatherForecastDataProvider(null, getIWeatherForecastDataProviderClientMock.Object);
            Action act2 = () => new WeatherForecastDataProvider(loggerMock.Object, null);

            //assert
            act1.Should().Throw<ArgumentNullException>();
            act2.Should().Throw<ArgumentNullException>();
        }


        [Theory]
        [InlineData("Munich")]
        [InlineData("Leipzig")]
        public async Task GetWeatherForecastDataByCity_EmptyForecast_ShouldReturnExpectedValues(string cityName)
        {
            //arrange
            var loggerMock = new Mock<ILogger<WeatherForecastDataProvider>>();
            var getIWeatherForecastDataProviderClientMock = new Mock<IWeatherForecastDataProviderClient>();
            getIWeatherForecastDataProviderClientMock.Setup(w => w.GetRawWeatherForecastDataByCity(cityName))
                .ReturnsAsync(new WeatherForecastResponse());

            var sut = new WeatherForecastDataProvider(loggerMock.Object, getIWeatherForecastDataProviderClientMock.Object);

            //act
            var result = await sut.GetWeatherForecastDataByCity(cityName);

            //assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Theory]
        [InlineData("Munich", 1, 12.33, 77,  4.33)]
        [InlineData("Leipzig", 1, 12.33, 77, 4.33)]
        public async Task GetWeatherForecastDataByCity_OneDayForecast_ShouldReturnExpectedValues(string cityName, 
            int expectedItems, double expectedTemperature, double expectedHumidity, double expectedWindSpeed)
        {
            //arrange
            var loggerMock = new Mock<ILogger<WeatherForecastDataProvider>>();
            var getIWeatherForecastDataProviderClientMock = new Mock<IWeatherForecastDataProviderClient>();
            getIWeatherForecastDataProviderClientMock.Setup(w => w.GetRawWeatherForecastDataByCity(cityName))
                .ReturnsAsync(GetOneDayWeatherForecastResponse());
            
            var sut = new WeatherForecastDataProvider(loggerMock.Object, getIWeatherForecastDataProviderClientMock.Object);

            //act
            var result = await sut.GetWeatherForecastDataByCity(cityName);

            //assert
            result.Should().NotBeNull();
            result.Should().HaveCount(expectedItems);
            var weatherForecastResult = result.First();
            weatherForecastResult.Temperature.Should().Be(expectedTemperature);
            weatherForecastResult.Humidity.Should().Be(expectedHumidity);
            weatherForecastResult.WindSpeed.Should().Be(expectedWindSpeed);
        }


        [Theory]
        [InlineData("Munich", 2, 11.75, 72, 3.75, 15, 90, 4)]
        [InlineData("Leipzig", 2, 11.75, 72, 3.75, 15, 90, 4)]
        public async Task GetWeatherForecastDataByCity_TwoDaysForecast_ShouldReturnExpectedValues(
            string cityName, int expectedItems,
            double expectedTemperatureDay1, double expectedHumidityDay1, double expectedWindSpeedDay1,
            double expectedTemperatureDay2, double expectedHumidityDay2, double expectedWindSpeedDay2)
        {
            //arrange
            var loggerMock = new Mock<ILogger<WeatherForecastDataProvider>>();
            var getIWeatherForecastDataProviderClientMock = new Mock<IWeatherForecastDataProviderClient>();
            getIWeatherForecastDataProviderClientMock.Setup(w => w.GetRawWeatherForecastDataByCity(cityName))
                .ReturnsAsync(GetTwoDaysWeatherForecastResponse());

            var sut = new WeatherForecastDataProvider(loggerMock.Object, getIWeatherForecastDataProviderClientMock.Object);

            //act
            var result = await sut.GetWeatherForecastDataByCity(cityName);

            //assert
            result.Should().NotBeNull();
            result.Should().HaveCount(expectedItems);
            var weatherForecastResultDay1 = result.First();
            weatherForecastResultDay1.Temperature.Should().Be(expectedTemperatureDay1);
            weatherForecastResultDay1.Humidity.Should().Be(expectedHumidityDay1);
            weatherForecastResultDay1.WindSpeed.Should().Be(expectedWindSpeedDay1);

            var weatherForecastResultDay2 = result.Last();
            weatherForecastResultDay2.Temperature.Should().Be(expectedTemperatureDay2);
            weatherForecastResultDay2.Humidity.Should().Be(expectedHumidityDay2);
            weatherForecastResultDay2.WindSpeed.Should().Be(expectedWindSpeedDay2);
        }


        private WeatherForecastResponse GetOneDayWeatherForecastResponse()
        {
            var result = new WeatherForecastResponse
            {
                Code = "200",
                Count = 3,
                Items = GetOneDayWeatherForecastInfoItems()
            };
            return result;
        }

        private IEnumerable<TimePointWeatherForecastInfo> GetOneDayWeatherForecastInfoItems()
        {
            var date = DateTime.Today;
            yield return new TimePointWeatherForecastInfo 
            { 
                ForecastDateTime = date,
                WindInfo = new WindInfo { Speed = 2 },
                Info = new WeatherInfo { Humidity = 60, Temperature = 10 }
            };
            yield return new TimePointWeatherForecastInfo
            {
                ForecastDateTime = date,
                WindInfo = new WindInfo { Speed = 4 },
                Info = new WeatherInfo { Humidity = 80, Temperature = 15 }
            };
            yield return new TimePointWeatherForecastInfo
            {
                ForecastDateTime = date,
                WindInfo = new WindInfo { Speed = 7 },
                Info = new WeatherInfo { Humidity = 90, Temperature = 12 }
            };
        }


        private WeatherForecastResponse GetTwoDaysWeatherForecastResponse()
        {
            var result = new WeatherForecastResponse
            {
                Code = "200",
                Count = 6,
                Items = GetTwoDaysWeatherForecastInfoItems()
            };
            return result;
        }

        private IEnumerable<TimePointWeatherForecastInfo> GetTwoDaysWeatherForecastInfoItems()
        {
            var date1 = DateTime.Today;
            var date2 = DateTime.Today.AddDays(1);
            yield return new TimePointWeatherForecastInfo
            {
                ForecastDateTime = date1,
                WindInfo = new WindInfo { Speed = 2 },
                Info = new WeatherInfo { Humidity = 60, Temperature = 10 }
            };
            yield return new TimePointWeatherForecastInfo
            {
                ForecastDateTime = date1,
                WindInfo = new WindInfo { Speed = 4 },
                Info = new WeatherInfo { Humidity = 80, Temperature = 15 }
            };
            yield return new TimePointWeatherForecastInfo
            {
                ForecastDateTime = date1,
                WindInfo = new WindInfo { Speed = 7 },
                Info = new WeatherInfo { Humidity = 90, Temperature = 12 }
            };

            yield return new TimePointWeatherForecastInfo
            {
                ForecastDateTime = date1,
                WindInfo = new WindInfo { Speed = 2 },
                Info = new WeatherInfo { Humidity = 60, Temperature = 10 }
            };
            yield return new TimePointWeatherForecastInfo
            {
                ForecastDateTime = date2,
                WindInfo = new WindInfo { Speed = 4 },
                Info = new WeatherInfo { Humidity = 90, Temperature = 15 }
            };
            yield return new TimePointWeatherForecastInfo
            {
                ForecastDateTime = date2,
                WindInfo = new WindInfo { Speed = 4 },
                Info = new WeatherInfo { Humidity = 90, Temperature = 15 }
            };
        }

    }
}
