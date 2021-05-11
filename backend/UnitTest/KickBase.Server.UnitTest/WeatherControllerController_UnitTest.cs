using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Appsfactory.WeatherForecast.Server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Appsfactory.WeatherForecast.SharedTypes.Contracts;

namespace Appsfactory.WeatherForecast.Server.UnitTest
{
    public class WeatherControllerController_UnitTest
    {
        [Fact]
        public void Constructor_MockParameters_ShouldNotThrow()
        {
            //arrange
            var loggerMock = new Mock<ILogger<WeatherController>>();
            var getWeatherForecastDataProviderMock = new Mock<ICanGetWeatherForecastData>();

            //act
            Action act = () => new WeatherController(loggerMock.Object, getWeatherForecastDataProviderMock.Object);

            //assert
            act.Should().NotThrow();
        }

        [Fact]
        public void Constructor_NullParameters_ShouldThrow()
        {
            //arrange
            var loggerMock = new Mock<ILogger<WeatherController>>();
            var getWeatherForecastDataProviderMock = new Mock<ICanGetWeatherForecastData>();
            //act
            Action act1 = () => new WeatherController(null, getWeatherForecastDataProviderMock.Object);
            Action act2 = () => new WeatherController(loggerMock.Object, null);

            //assert
            act1.Should().Throw<ArgumentNullException>();
            act2.Should().Throw<ArgumentNullException>();
        }
    }
}
