using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .Build();
            var mqttPipeLinePort = int.Parse(config["AppSettings:KestrelSettings:MqttPipeLinePort"]);
            var httpPipeLinePort = int.Parse(config["AppSettings:KestrelSettings:HttpPipeLinePort"]);
            var httpsPipeLinePort = int.Parse(config["AppSettings:KestrelSettings:HttpsPipeLinePort"]);

            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseKestrel(kestrelServerOptions =>
                {
                    // Configure the port for the MQTT PipeLine
                    kestrelServerOptions.ListenAnyIP(mqttPipeLinePort,
                        listenOptions => listenOptions.UseMqtt());
                    // Configure the port for the Default HTTP PipeLine
                    kestrelServerOptions.ListenAnyIP(httpPipeLinePort);
                    // Configure the port for the Default HTTPS PipeLine
                    kestrelServerOptions.ListenAnyIP(httpsPipeLinePort,
                        listenOptions => listenOptions.UseHttps());
                }).UseStartup<Startup>();
            });
        }
    }
}