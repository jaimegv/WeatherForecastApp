using AutoMapper.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Net.Http;
using Polly;
using Polly.Extensions.Http;
using Appsfactory.WeatherForecast.SharedTypes.Contracts;
using Appsfactory.WeatherForecast.SharedTypes.Configuration;
using Appsfactory.OpenWeatherForecastService.Services;
using Appsfactory.WeatherForecast.SharedTypes.JsonConverters;

namespace Appsfactory.WeatherForecast.Server
{
    public class Startup
    {
        private const string CorsPolicyName = "AcceptAllPolicy";
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddJsonOptions(opts =>
                        {
                            opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                            opts.JsonSerializerOptions.Converters.Add(new JsonDateTimeConverter());
                        });

            RegisterWeatherForecastServices(services);
            RegisterWeatherForecastClientServices(services);
            AddCorsPolicies(services);
        }

        private void AddCorsPolicies(IServiceCollection services) =>
            services.AddCors(o => o.AddPolicy(CorsPolicyName, builder =>
            {
                builder.AllowAnyOrigin()
                .SetIsOriginAllowed((host) => true)
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

        private void RegisterWeatherForecastServices(IServiceCollection services)
        {
            services.Configure<ExternalWeatherApiConfiguration>(Configuration.GetSection(nameof(ExternalWeatherApiConfiguration)));
            services.AddSingleton<ExternalWeatherApiConfiguration>(sp => sp.GetRequiredService<IOptions<ExternalWeatherApiConfiguration>>().Value);
            services.AddTransient<ICanGetWeatherForecastData, WeatherForecastDataProvider>();
        }

        private void RegisterWeatherForecastClientServices(IServiceCollection services) => 
            services.AddHttpClient<IWeatherForecastDataProviderClient, OpenWeatherForecastHttpClient>((sp, client) =>
                {
                    client.BaseAddress = new Uri(sp.GetService<ExternalWeatherApiConfiguration>().Url);
                });

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(CorsPolicyName);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
