using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using Serilog;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Appsfactory.WeatherForecast.Server
{
    public class Program
    {
        private const string ConfigurationFolderName = "Configuration";
        private const string SettingsFileRootName = "Appsfactory.WeatherForecast.Server.Settings";

        private static string _environment;
        public static void Main(string[] args)
        {
            _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            var configuration = new ConfigurationBuilder()
               .AddJsonFile(Path.Combine(ConfigurationFolderName, $"{SettingsFileRootName}.json"), optional: false, reloadOnChange: true)
               .AddJsonFile(Path.Combine(ConfigurationFolderName, $"{SettingsFileRootName}.{_environment}.json"), optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
               .Build();

            Log.Logger = new LoggerConfiguration()
               .Enrich.FromLogContext()
               .ReadFrom.Configuration(configuration)
               .CreateLogger();

            try
            {
                Log.Information("Starting host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureAppConfiguration((hostingContext, configuration) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    configuration
                        .SetBasePath(env.ContentRootPath)
                        .AddJsonFile(Path.Combine(ConfigurationFolderName, $"{SettingsFileRootName}.json"), optional: false, reloadOnChange: true)
                        .AddJsonFile(Path.Combine(ConfigurationFolderName, $"{SettingsFileRootName}.{_environment}.json"), optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
