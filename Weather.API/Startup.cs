using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using Weather.API.Adapters;
using Weather.API.Configurations;
using Weather.API.Infrastructure;
using Weather.API.Models.Dto;
using Weather.API.Persistence;
using Weather.API.Processors;

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
            services.AddDbContext<WeatherDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:WeatherDB"]));
            services.AddSingleton<IApplicationConfiguration>(x => new ApplicationConfiguration(Configuration));
            
            services.AddScoped<IWeatherServiceAdapter, OpenWeathermapAdapter>();  
            services.AddScoped<IRequestSender<dynamic>, HttpRequestSender<dynamic>>();
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

            GenerateDB(app);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void GenerateDB(IApplicationBuilder app)
        {
            try
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    using (var context = serviceScope.ServiceProvider.GetService<WeatherDbContext>())
                    {
                        context.Database.EnsureCreated();
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO: handle 
                throw;
            }
        }

    }
}
