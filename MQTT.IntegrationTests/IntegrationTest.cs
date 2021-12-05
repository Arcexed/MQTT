using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MQTT.Api;
using MQTT.Api.Contracts.v1.Request.AccountController;
using MQTT.Api.Contracts.v1.Request.DeviceController;
using MQTT.Api.Contracts.v1.Response.AccountController;
using MQTT.Api.Options;
using MQTT.Data;
using MQTTnet.AspNetCore.Extensions;
using Xunit;

namespace MQTT.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        
        public IntegrationTest()
        {
            var appfactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseKestrel(kestrelServerOptions =>
                    {
                        kestrelServerOptions.ListenAnyIP(1883,
                            listenOptions => listenOptions.UseMqtt());
                    });
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(MQTTDbContext));
                        services.AddDbContext<MQTTDbContext>(options =>
                        {
                            options.UseSqlServer("Data Source=178.54.86.113,14330;Initial Catalog=mqttdb_new;User ID=SA;Password=19Andrei19");
                        });
                    });

                    /*builder.Configure(app =>
                    {
                        app.UseDeveloperExceptionPage();
                    });*/
                });
            TestClient = appfactory.CreateClient();
        }

        protected async Task<HttpResponseMessage> CreateDeviceAsync(CreateDeviceRequest request)
        {
            return await TestClient.PutAsJsonAsync(ApiRoutes.Devices.Create, request);
        }

        protected async Task<HttpResponseMessage> GetAllDevicesAsync()
        {
            return await TestClient.GetAsync(ApiRoutes.Devices.GetAll);
        }

        protected async Task<HttpResponseMessage> GetDeviceByIdAsync(Guid deviceId)
        {
            return await TestClient.GetAsync(ApiRoutes.Devices.Get.Replace("{deviceId}", deviceId.ToString("D"))); 
        }
        protected async Task<HttpResponseMessage> DeleteDeviceAsync(Guid deviceId)
        {
            return await TestClient.DeleteAsync(ApiRoutes.Devices.Delete.Replace("{deviceId}", deviceId.ToString("D")));
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }
        private async Task<string> GetJwtAsync()
        {
            var request = new UserLoginRequest()
            {
                Email = "arcex.off@gmail.com",
                Password = "123456",
            };
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Account.Login, request);
            var registrationResponse = await response.Content.ReadAsAsync<UserLoginResponse>();
            return registrationResponse.AccessToken;
        }
    }
}