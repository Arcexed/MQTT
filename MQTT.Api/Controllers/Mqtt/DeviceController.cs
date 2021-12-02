#region Using Imports

using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using MQTT.Api.Services;
using MQTT.Data;
using MQTT.Data.Entities;
using MQTTnet;
using MQTTnet.AspNetCore.AttributeRouting;

#endregion Using Imports

namespace MQTT.Api.Controllers.Mqtt
{
    [MqttController] // Generate MQTT Attribute Routing for the Controller
    [MqttRoute("[controller]")] // Defines the Route Prefix for the Controller
    public class DeviceController : MqttBaseController // Inherit from MqttBaseController for convenience functions
    {
        // Initialize the MQTT Controller with full dependency injection support (Like normal AspNetCore controllers)
        public DeviceController(LoggerService loggerService, MqttService mqttService, MQTTDbContext db)
        {
            _loggerService = loggerService;
            _mqttService = mqttService;
            _db = db;
        }


        [MqttRoute("{deviceName}/Measurements")] // Generate MQTT Attribute Routing for this Topic
        public Task PublishWeatherReport(string deviceName)
        {
            var client = _mqttService.ConnectedClients.FirstOrDefault(d => d.Id == MqttContext.ClientId);
            if (client != null)
            {
                if (client.Username != deviceName)
                {
                    MqttContext.CloseConnection = true;
                    return BadMessage();
                }

                var device = _db.Devices.FirstOrDefault(d => d.Name == deviceName);

                if (device == null)
                {
                    MqttContext.CloseConnection = true;
                    return BadMessage();
                }

                var measurement = ParseMeasurementFromMqttMessage(Message);
                if (measurement != null)
                {
                    measurement.Date = DateTime.Now;
                    measurement.Id = Guid.NewGuid();
                    measurement.Device = device;
                    _db.Measurements.Add(measurement);
                    _db.SaveChanges();
                    _loggerService.Log(
                        $"It's {measurement.Id} {measurement.Date} {measurement.Device.Name} send measurement");
                }
                else
                {
                    _loggerService.LogEventDevice(device,
                        $"It's {client.Username} {client.Id}  unsuccessfully send measurement {measurement.Id} (Measurement ID)");
                    _loggerService.Log(
                        $"It's {client.Username} {client.Id}  unsuccessfully send measurement {measurement.Id} (Measurement ID)");
                    MqttContext.CloseConnection = true;
                    return BadMessage();
                }
            }

            return Ok();
        }

        private Measurement? ParseMeasurementFromMqttMessage(MqttApplicationMessage message)
        {
            var payload = message.Payload;
            if (payload != null)
                try
                {
                    var measurement =
                        JsonSerializer.Deserialize<Measurement>(payload,
                            new JsonSerializerOptions(JsonSerializerDefaults.Web));
                    return measurement;
                }
                catch (Exception ex)
                {
                    _loggerService.Log($"{message.Topic} not parse {ex.Message}");
                }

            return null;
        }

        #region Variable Declarations

        // Default Variable Initialization
        private readonly LoggerService _loggerService;
        private readonly MqttService _mqttService;
        private readonly MQTTDbContext _db;
        private const string MqttNetPubWeatherReport = "Device/" + "{deviceName}" + "/Measurements";

        #endregion Variable Declarations
    }
}