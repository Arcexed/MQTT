using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MQTT.Api.Controllers.Api
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        UserManager<IdentityUser> _userManager;
        public AdminController(UserManager<IdentityUser> manager)
        {
            _userManager = manager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_userManager.Users.ToList());
        }
    }
}