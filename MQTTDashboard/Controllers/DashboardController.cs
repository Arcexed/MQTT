using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Models.Models;

namespace MQTTDashboard.Controllers
{
    public class DashboardController : Controller
    {
        private readonly mqttdb_newContext _db;
        private readonly ILogger<DashboardController> _logger;
        public DashboardController(mqttdb_newContext db,ILogger<DashboardController> logger)
        {
            _db = db;
            _logger = logger;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Device()
        {
            //.Include(d=>d.Devices)
            //.Include(d=>d.IdUserNavigation)
            //.Include(d=>d.EventsDevices)

            string username = User.Identity.Name;
            var devices = _db.Users.Include(d=>d.Devices).First(d=>d.Username==username).Devices; 
            return View(devices);
        }   

        [Authorize]
        [Route("{DeviceName}")]
        public IActionResult Device(string DeviceName)
        {

            return View();
        }

    }
}
