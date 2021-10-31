#region Using Imports

using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MQTT.Api.Models;
using MQTT.Api.Services;
using MQTT.Data;
using MQTT.Data.Entities;
using MQTTnet;
using MQTTnet.AspNetCore.AttributeRouting;
using MQTTnet.Extensions;

#endregion Using Imports

namespace MQTT.Api.Controllers.Mqtt
{
    [MqttController] // Generate MQTT Attribute Routing for the Controller
    [MqttRoute("Measurements")] // Defines the Route Prefix for the Controller
    public class MeasurementsController : MqttBaseController // Inherit from MqttBaseController for convenience functions
    {
        #region Variable Declarations

        // Default Variable Initialization
        private readonly AppSettings _appSettings;
        private readonly LoggerService _loggerService;
        private readonly MqttService _mqttService;
        private readonly MQTTDbContext _db;
        private const string Pub = "publish/";
        private const string MqttNetPubWeatherReport = Pub + "{deviceName}";
        #endregion Variable Declarations
        
        // Initialize the MQTT Controller with full dependency injection support (Like normal AspNetCore controllers)
        public MeasurementsController(AppSettings appSettings, LoggerService loggerService, MqttService mqttService, MQTTDbContext db)
        {
            _appSettings = appSettings;
            _loggerService = loggerService;
            _mqttService = mqttService;
            _db = db;
        }
        
        
        [MqttRoute(MqttNetPubWeatherReport)] // Generate MQTT Attribute Routing for this Topic
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
                    _loggerService.Log($"It's {client.Username} {client.Id}  unsuccessfully send measurement {measurement.Id} (Measurement ID)");
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
            {
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
            }
            return null;
        }
    }
}