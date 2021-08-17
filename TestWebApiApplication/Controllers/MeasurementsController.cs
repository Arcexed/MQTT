using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MQTTWebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MQTTWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MeasurementsController : ControllerBase
    {
        private readonly ILogger<MeasurementsController> _logger;
        private readonly MqttdbContext _db;
        public MeasurementsController(ILogger<MeasurementsController> logger, MqttdbContext db)
        {
            _logger = logger;
            _db = db;
        }
        // GET api/<MeasurementsController>/5
        [HttpGet("{deviceName}/Add")]
        public string AddMeasurements(string deviceName, float atmosphericPressure, float temperature, float airHumidity, float lightLevel, float smokeLevel)
        {
            
                var device = _db.Devices.Where(d => d.Name == deviceName).FirstOrDefault();
                if (device != null)
                {
                    var measurement = new Measurement()
                    {
                        Device = device,
                        AtmosphericPressure = atmosphericPressure,
                        Temperature = temperature,
                        AirHumidity = airHumidity,
                        LightLevel = lightLevel,
                        SmokeLevel = smokeLevel,
                        Date = DateTime.Now
                    };
                    _db.Measurements.Add(measurement);
                    _db.SaveChanges();
                    return "Success";
                }
                else
                {
                    return "Device does not exists";
                }
        }

        [HttpGet("{deviceName}/Get")]
        public IEnumerable<Measurement> GetMeasurements(string deviceName)
        {
            return _db.Measurements.Where(m => m.Device.Name == deviceName);
        }
    }
}
