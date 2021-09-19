#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MQTT.Data;
using MQTT.Data.Entities;

// ReSharper disable NotAccessedField.Local

#endregion

namespace MQTT.Dashboard.Controllers
{
    [Route("{controller}")]
    public class DashboardController : Controller
    {
        private readonly MQTTDbContext _db;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(MQTTDbContext db, ILogger<DashboardController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [Authorize]
        [Route("Index")]
        public IActionResult Index()
        {
            return View(_db);
        }

        [Authorize]
        [Route("Device")]
        public async Task<IActionResult> Device()
        {
            var username = User.Identity?.Name;
            var devices = (await _db.Users.FirstOrDefaultAsync(x =>
                x.Username == username)).Devices.ToList();

            return View(devices);
        }

        [Authorize]
        [Route("Device/{deviceName}")]
        public async Task<IActionResult> Device(string deviceName, string span)
        {
            if (deviceName == "" || span == "1") return NotFound();
            var username = User.Identity?.Name;
            var device = (await _db.Users.FirstOrDefaultAsync(x => x.Username == username))?
                .Devices.FirstOrDefault(x => x.Name == deviceName);
            return View(new List<Device> { device });
        }
    }
}