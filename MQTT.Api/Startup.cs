#region

using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTT.Api.Controllers.Mqtt;
using MQTT.Api.Extensions;
using MQTT.Api.Models;
using MQTT.Api.Services;
using MQTT.Data;
using MQTTnet.AspNetCore;
using MQTTnet.AspNetCore.AttributeRouting;
using MQTTnet.AspNetCore.Extensions;

// ReSharper disable UnusedAutoPropertyAccessor.Local

#endregion

namespace MQTT.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        private AppSettings _appSettings;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AuthenticationConfiguration();
            services.DatabaseConfiguration(Configuration);
            services.AutoMapperConfiguration();
            services.SwaggerConfiguration();
            /////////
            _appSettings = new AppSettings();
            Configuration.Bind("AppSettings", _appSettings);
            services.AddSingleton(_appSettings);

            // Allow CORS
            services.MqttConfiguration();
            // Add the Controllers
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            );

            app.UseDeveloperExceptionPage();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                c.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapConnectionHandler<MqttConnectionHandler>(
                    "/mqtt",
                    httpConnectionDispatcherOptions =>
                        httpConnectionDispatcherOptions.WebSockets.SubProtocolSelector =
                            protocolList => protocolList.FirstOrDefault() ?? string.Empty);

            });
            app.UseMqttServer(server =>
                app.ApplicationServices.GetRequiredService<MqttService>().ConfigureMqttServer(server));


            app.Run(async context => { await context.Response.WriteAsync("Unknown request"); });
        }
    }
}