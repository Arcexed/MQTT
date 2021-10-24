using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MQTT.Data.Entities;
using MQTT.Shared.DBO;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Diagnostics;
using MQTTnet.Implementations;

namespace MQTT.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
         //   await Http();
            await Mqtt();
          //  Task.WaitAll();
        }

        private static async Task Http()
        {
            for (int i = 0; i < 10000; i++)
            {
                HttpClient client = new HttpClient();
                Random rnd = new Random();
                string request =
                    $"http://localhost:5000/api/Measurements/TestDevice/Add";
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("mqttToken", "3db1f91c8ba1475fb24c3c0ce62e1415"),
                    new KeyValuePair<string, string>("atmosphericPressure", rnd.Next(500, 700).ToString()),
                    new KeyValuePair<string, string>("temperature", rnd.Next(40).ToString()),
                    new KeyValuePair<string, string>("airHumidity", rnd.Next(100).ToString()),
                    new KeyValuePair<string, string>("lightLevel", rnd.Next(100).ToString()),
                    new KeyValuePair<string, string>("smokeLevel", rnd.Next(100).ToString()),
                    new KeyValuePair<string, string>("radiationLevel", rnd.Next(50).ToString())
                });
                Uri uri = new Uri(request);
                await client.PostAsync(request, content);

            }
        }

        private static async Task Mqtt()
        {
            var factory = new MqttFactory();
            var mqttClient = factory.CreateMqttClient();
            string clientId = "24ba0e92-5051-4dea-879c-d83e58528095";
            string mqttURI = "127.0.0.1";
            string mqttUser = "TestDevice";
            string mqttPassword = "3db1f91c8ba1475fb24c3c0ce62e1415";
            int mqttPort = 1883;
            bool mqttSecure = false;
            var options = new MqttClientOptionsBuilder()
                .WithClientId(clientId)
                .WithTcpServer(mqttURI)
                .WithCredentials(mqttUser, mqttPassword)
                .WithCleanSession()
                .Build();
            await mqttClient.ConnectAsync(options, CancellationToken.None); // Since 3.0.5 with CancellationToken
            Random rnd = new Random();
            for (int i = 0; i < 1000000; i++)
            {
                var json =
                        $"{{\"atmosphericPressure\":{rnd.Next(600,700)},\"temperature\":{rnd.Next(30)},\"airHumidity\":{rnd.Next(100)},\"lightLevel\":{rnd.Next(100)},\"smokeLevel\":{rnd.Next(100)},\"radiationLevel\":{rnd.Next(30)}}}";
                await mqttClient.PublishAsync($"Measurements/publish/{mqttUser}",json);
                Thread.Sleep(1);
            }
        }
    }
}