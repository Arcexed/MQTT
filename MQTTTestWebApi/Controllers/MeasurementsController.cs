using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MQTTWebApi.Models;
using MQTTWebApi.Static;

namespace MQTTWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeasurementsController : ControllerBase
    {
        
        private readonly ILogger<MeasurementsController> _logger;

        public MeasurementsController(ILogger<MeasurementsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        
        public IEnumerable<Device> Get(int limit)
        {
            using (MqttDBContext db = new MqttDBContext())
            {
                return db.Device.ToArray().Take(limit);
                //return Enumerable.Range(1, limit).Select(x => db.Device).Take().ToArray();
                //    .Take(limit).ToArray();
                //// получаем объекты из бд и выводим на консоль
                //var users = db.Device.ToList();
                //Console.WriteLine("Список объектов:");
               
            }
            //return Enumerable.Range(1, limit).Select(index => new WeatherForecast
            //    {
            //        Date = DateTime.Now.AddDays(index),
            //        TemperatureC = rng.Next(-20, 55),
            //        Summary = Summaries[rng.Next(Summaries.Length)],
            //        RadiationRoentgen = rng.Next(50)

            //    })
            //    .Take(limit).ToArray();

        }
    }
}
