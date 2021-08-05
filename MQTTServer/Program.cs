using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTServer
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        async Task ServerListen()
        {
            MqttClient mqttClient = new Mqtt.Client.MqttClient();
            var result = await mqttClient.ConnectAsync("127.0.0.1", 1883);
            Console.WriteLine($"connect {result.Success}");
            if (result.Success)
            {
                await mqttClient.SubscribeAsync("test/1", DotNetty.Codecs.Mqtt.Packets.QualityOfService.AtLeastOnce, (packet) =>
                {
                    var pubpacket = (DotNetty.Codecs.Mqtt.Packets.PublishPacket)packet;
                    Console.WriteLine(pubpacket.Payload.GetString(0, pubpacket.Payload.WriterIndex, UTF8Encoding.UTF8));
                });
                await Task.Delay(1000);
                for (int i = 0; i < 8; i++)
                {
                    await mqttClient.PublishAsync("test/1", UTF8Encoding.UTF8.GetBytes(i.ToString()));
                    await Task.Delay(1000);
                }
                await Task.Delay(1000);
                await mqttClient.DisconnectAsync();

            }
        }
}
