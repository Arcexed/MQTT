using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MQTT.Data;

namespace MQTT.Api.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class HealthyController : Controller
    {
        private readonly MQTTDbContext _db;
        private readonly ILogger<HealthyController> _logger;

        public HealthyController(MQTTDbContext db, ILogger<HealthyController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (_db.Database.CanConnect())
                return Ok("DB Connect: true");
            return BadRequest("DB Connect: false");
        }
    }
}