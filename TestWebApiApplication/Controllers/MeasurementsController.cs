﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MQTTWebApi.Models;
using MQTTWebApi.Models.ForReport;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MQTTWebApi.Controllers
{
    [Route("[controller]")]
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
                    Response.StatusCode = 404;
                    return "Device does not exists";
                }
        }

        [HttpGet("{deviceName}/Get")]
        public IEnumerable<MeasurementViewModel> GetMeasurements(string deviceName)
        {
            Device device = _db.Devices.Where(d => d.Name == deviceName).FirstOrDefault();
            if (device != null)
            {
                IEnumerable<MeasurementViewModel> measurementViewModels =
                    _mapper.Map<IEnumerable<Measurement>, IEnumerable<MeasurementViewModel>>(_db.Measurements
                        .Where(m => m.Device.Name == deviceName).ToArray());

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
