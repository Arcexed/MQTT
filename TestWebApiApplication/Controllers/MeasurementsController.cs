using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApiApplication.Models;

namespace TestWebApiApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeasurementsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<MeasurementsController> _logger;

        public MeasurementsController(ILogger<MeasurementsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get(int limit)
        {
            var rng = new Random();
            return Enumerable.Range(1, limit).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                RadiationRoentgen = rng.Next(50)
                
            })
                .Take(limit).ToArray();
        }
    }
}
