using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MQTTnet.AspNetCore.Extensions;

namespace MQTT.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseKestrel(kestrelServerOptions =>
                {
                    kestrelServerOptions.ListenAnyIP(1883,
                        listenOptions => listenOptions.UseMqtt());
                }).UseStartup<Startup>();
            });
        }
    }
}