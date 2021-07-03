using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Weather.API.Adapters;
using Weather.API.Configurations;
using Weather.API.Infrastructure;
using Weather.API.Models.Dto;
using Weather.API.Persistence;
using Weather.API.Services;

namespace Weather.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Weather.API", Version = "v1" });
            });

            services.AddScoped<IWeatherProcessors, WeatherProcessors>();


            services.AddScoped<IWeatherRepository, WeatherRepository>();
            services.AddSingleton<IApplicationConfiguration>(x => new ApplicationConfiguration(Configuration));
            
            services.AddScoped<IWeatherServiceAdapter, OpenWeathermapAdapter>();  
            services.AddScoped<IRequestSender<WeatherResponse>, HttpRequestSender<WeatherResponse>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
