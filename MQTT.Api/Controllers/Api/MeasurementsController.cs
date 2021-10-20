#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MQTT.Data;
using MQTT.Data.Entities;
using MQTT.Shared.DBO;
using Swashbuckle.AspNetCore.Annotations;

// ReSharper disable NotAccessedField.Local

#endregion

namespace MQTT.Api.Controllers.Api
{
    [Route("/api/[controller]")]
    [ApiController]
    public class MeasurementsController : ControllerBase
    {
        private readonly MQTTDbContext _db;
        private readonly ILogger<MeasurementsController> _logger;
        private readonly IMapper _mapper;

        public MeasurementsController(ILogger<MeasurementsController> logger, MQTTDbContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }
        
        
        [HttpPost]
        [AllowAnonymous]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), 200)]
        [SwaggerOperation("Authentication user")]
        [Route("{deviceName}/Add")]
        public async Task<IActionResult> AddMeasurements([Required] string deviceName,[Required] string mqttToken, float atmosphericPressure,
            float temperature, float airHumidity, float lightLevel, float smokeLevel, float radiationLevel)
        {
            var device = _db.Devices.FirstOrDefault(d => d.Name == deviceName && d.MqttToken == mqttToken);
            if (device != null)
            {
                var measurement = new Measurement
                {
                    AtmosphericPressure = atmosphericPressure,
                    Temperature = temperature,
                    AirHumidity = airHumidity,
                    LightLevel = lightLevel,
                    SmokeLevel = smokeLevel,
                    RadiationLevel = radiationLevel,
                    Date = DateTime.Now,
                    Device = device
                };
                _db.Measurements.Add(measurement);
                await _db.SaveChangesAsync();
                StringBuilder sb = new();
                sb.Append($"{DateTime.Now.ToString("O")} Device {deviceName} {mqttToken} add record ");
                sb.Append($"AP={atmosphericPressure}");
                sb.Append($"T={temperature}");
                sb.Append($"AH={airHumidity}");
                sb.Append($"LL={lightLevel}");
                sb.Append($"SL={smokeLevel}");
                sb.Append($"RL={radiationLevel}");
                sb.Append($"({Request.Query}) IP:{HttpContext.Request.Host.Host}");
                _logger.Log(LogLevel.Information, sb.ToString());
                return Ok("Success");
            }

            _logger.Log(LogLevel.Information,
                $"{DateTime.Now.ToString("O")} Device {deviceName} {mqttToken} fail (device does not find) ({Request.Query}) IP:{HttpContext.Request.Host.Host}");
            return NotFound("Device does not exists");
        }

        [Authorize(Roles = "Root, Admin")]
        [HttpGet("{deviceName}/Get")]
        public async Task<IEnumerable<MeasurementViewModel>?> GetMeasurements([Required] string deviceName)
        {
            Device device = await _db.Devices.Where(d => d.Name == deviceName).FirstOrDefaultAsync();
            if (device != null)
            {
                var measurementViewModels =
                    _mapper.Map<IEnumerable<Measurement>, IEnumerable<MeasurementViewModel>>(device.Measurements);

                //IEnumerable<MeasurementViewModel> measurementViewModels = 
                //    _mapper.Map<IEnumerable<Measurement>, IEnumerable<MeasurementViewModel>>(_db.Measurements
                //        .Where(m => m.Device.Name == deviceName).Take(limit != 0 ? (limit < 1000 ? limit : 1000) : 10));
                return measurementViewModels;
            }

            Response.StatusCode = 404;
            return null;
        }
    }
}