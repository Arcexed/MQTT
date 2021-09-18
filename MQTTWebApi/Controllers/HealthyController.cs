using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Models;

namespace MQTTWebApi.Controllers
{
    [Route("/api/[controller]")]
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
        public IActionResult Index()
        {
            
            if (_db.Database.CanConnect())
            {
                return Ok("DB Connect: true");

            }
            else
            {
                return BadRequest("DB Connect: false");
            }
        }
    }
}
