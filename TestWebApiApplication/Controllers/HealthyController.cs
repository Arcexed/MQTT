using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.DbModels;

namespace MQTTWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthyController : Controller
    {
        private readonly mqttdb_newContext _db;
        private readonly ILogger<HealthyController> _logger;
        public HealthyController(mqttdb_newContext db,ILogger<HealthyController> logger)
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
