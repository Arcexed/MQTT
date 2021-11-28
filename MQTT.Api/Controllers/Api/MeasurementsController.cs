#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MQTT.Api.Services;
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
        private readonly LoggerService _loggerService;
        private readonly MQTTDbContext _db;
        private readonly IMapper _mapper;

        public MeasurementsController(MQTTDbContext db, IMapper mapper, LoggerService loggerService)
        {
            _db = db;
            _mapper = mapper;
            _loggerService = loggerService;
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
                sb.Append($"Device {deviceName} {mqttToken} add record ");
                sb.Append($"AP={atmosphericPressure}");
                sb.Append($"T={temperature}");
                sb.Append($"AH={airHumidity}");
                sb.Append($"LL={lightLevel}");
                sb.Append($"SL={smokeLevel}");
                sb.Append($"RL={radiationLevel}");
                sb.Append($"({Request.Query}) IP:{HttpContext.Request.Host.Host}");
                _loggerService.Log(sb.ToString());
                return Ok("Success");
            }

            _loggerService.Log($"Device {deviceName} {mqttToken} fail (device does not find) ({Request.Query}) IP:{HttpContext.Request.Host.Host}");
            return NotFound("Device does not exists");
        }

        [Authorize(Roles = "Root, Admin")]
        [HttpGet("{deviceName}/Get")]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<MeasurementViewModel>), 200)]
        public async Task<IActionResult> GetMeasurements([Required] string deviceName, DateTime? startDate, DateTime? endDate, int page = 1, int limit = 25)
        {
            if (limit > 1000)
            {
                return BadRequest("Limit must be less than 1000");
            }

            if (startDate > endDate)
            {
                return BadRequest("Start date must be greater than end date");
            }

            if (page < 1)
            {
                return BadRequest("Page must be greater than 1");
            }
            
            Device device = await _db.Devices.Where(d => d.Name == deviceName).FirstOrDefaultAsync();
            if (device != null)
            {
                var measurementViewModels =
                    _mapper.Map<IEnumerable<Measurement>, IEnumerable<MeasurementViewModel>>
                        (_db.Measurements
                            .Where(d=>d.Device== device && ((endDate!=null && startDate!=null) ? d.Date>startDate && d.Date<endDate : true))
                            .Skip(limit*(page-1))
                            .OrderByDescending(d=>d.Date)
                            .Take(limit));
            
                //IEnumerable<MeasurementViewModel> measurementViewModels = 
                //    _mapper.Map<IEnumerable<Measurement>, IEnumerable<MeasurementViewModel>>(_db.Measurements
                //        .Where(m => m.Device.Name == deviceName).Take(limit != 0 ? (limit < 1000 ? limit : 1000) : 10));
                return Ok(measurementViewModels);
            }

            return NotFound();
        }
    }
}