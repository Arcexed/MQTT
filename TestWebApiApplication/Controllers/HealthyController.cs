using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MQTTWebApi.Models;

namespace MQTTWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthyController : Controller
    {
        private readonly MqttdbContext _db;
        private readonly ILogger<HealthyController> _logger;
        public HealthyController(MqttdbContext db,ILogger<HealthyController> logger)
        {
            _db = db;
            _logger = logger;
        }
        [HttpGet]
        public string Index()
        {
            
            if (_db.Database.CanConnect())
            {
                return "DB Connect: true";

            }
            else
            {
                return "DB Connect: false";
            }
        }
    }
}
