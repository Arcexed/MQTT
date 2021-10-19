using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MQTTnet.AspNetCore.AttributeRouting;

namespace MQTT.BrokerWeb.Controllers
{
    [MqttController]
    [MqttRoute("[controller]")]
    public class MeasurementsController : MqttBaseController
    {
        private readonly ILogger<MeasurementsController> _logger;
        public MeasurementsController(ILogger<MeasurementsController> logger)
        {
            _logger = logger;
        }
        // [MqttRoute("{deviceName:string}")]
        [MqttRoute("{zipCode}")]
        public Task WeatherReport(int zipCode)
        {
            
            var payload = BitConverter.ToString(Message.Payload);
            var temperature = BitConverter.ToDouble(Message.Payload);
            Console.WriteLine($"{zipCode} payload:{payload} temperature:{temperature}");
            
            _logger.LogInformation($"It's {temperature} degrees in Hollywood");

            // Example validation
            if (temperature <= 0 || temperature >= 130)
            {
                // Prevents the message from being published on the topic to any subscribers
                return BadMessage();
            }

            // Publish the message to all subscribers on this topic
            return Ok();
        }
    }
}