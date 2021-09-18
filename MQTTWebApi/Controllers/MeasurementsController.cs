using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Models;
using Models.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MQTTWebApi.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class MeasurementsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MeasurementsController> _logger;
        private readonly MqttdbContext _db;
        public MeasurementsController(ILogger<MeasurementsController> logger, MqttdbContext db,IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }
        // GET api/<MeasurementsController>/5
        [HttpGet("{deviceName}/Add")]
        public async Task<IActionResult> AddMeasurements(string deviceName, float atmosphericPressure, float temperature, float airHumidity, float lightLevel, float smokeLevel,float radiationLevel)
        {
            
                var device = _db.Devices.FirstOrDefault(d => d.Name == deviceName);
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
                        RadiationLevel = radiationLevel,
                        Date = DateTime.Now
                    };
                    await _db.Measurements.AddAsync(measurement);
                    await _db.SaveChangesAsync();
                    return Ok("Success");
                }
                else
                {
                    Response.StatusCode = 404;
                    return NotFound("Device does not exists");
                }
        }

        [Authorize(Roles = "Root, Admin")]
        [HttpGet("{deviceName}/Get")]
        public async Task<IEnumerable<MeasurementViewModel>?> GetMeasurements(string deviceName)
        {
            Device device = await _db.Devices.Where(d => d.Name == deviceName).FirstOrDefaultAsync();
            if (device != null)
            {
                IEnumerable<MeasurementViewModel> measurementViewModels =
                    _mapper.Map<IEnumerable<Measurement>, IEnumerable<MeasurementViewModel>>(await _db.Measurements
                        .Where(m => m.Device.Name == deviceName).ToArrayAsync());

                //IEnumerable<MeasurementViewModel> measurementViewModels = 
                //    _mapper.Map<IEnumerable<Measurement>, IEnumerable<MeasurementViewModel>>(_db.Measurements
                //        .Where(m => m.Device.Name == deviceName).Take(limit != 0 ? (limit < 1000 ? limit : 1000) : 10));
                return measurementViewModels;
            }
            else
            {
                Response.StatusCode = 404;
                return null;
            }   
        }
    }
}
