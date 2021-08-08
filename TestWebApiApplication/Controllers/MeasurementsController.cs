using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MQTTWebApi.Models;
using MQTTWebApi.Static;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MQTTWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementsController : ControllerBase
    {

        // GET api/<MeasurementsController>/5
        [HttpGet("{deviceName}/Add")]
        public string Get(string deviceName,float atmosphericPressure, float temperature,float airHumidity, float lightLevel, float smokeLevel)
        {
            using (MqttDBContext db = new MqttDBContext())
            {
                var device = db.Device.Where(d => d.Name == deviceName).FirstOrDefault();
                if (device != null)
                {
                    var measurement = new Measurements(device,atmosphericPressure,temperature,airHumidity,lightLevel,smokeLevel);
                    db.Measurements.Add(measurement);
                    db.SaveChanges();
                    return "Success";
                }
                else
                {
                    return "Device does not exists";
                }
            }
            return "value";
        }
        
        
    }
}
