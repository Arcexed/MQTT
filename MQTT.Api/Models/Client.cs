using System;

namespace MQTT.Api.Models
{
    public class Client
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string MqttToken { get; set; }
        public string Ip { get; set; }
        public DateTime ConnectedTime { get; set; }
    }
}