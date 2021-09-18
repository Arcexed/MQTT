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

namespace Models.Controllers
{
    [Route("{controller}")]
    public class DashboardController : Controller
    {
        private readonly MqttdbContext _db;
        private readonly ILogger<DashboardController> _logger;
        public DashboardController(MqttdbContext db,ILogger<DashboardController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [Authorize]
        [Route("Index")]

        public IActionResult Index()
        {
            string username = User.Identity.Name;
            var eventsDevices = _db.EventsDevices.Where(d => d.Device.User.Username == username);   
            return View(_db);
        }


        [Authorize]
        [Route("Device")]
        public IActionResult Device()
        {
            //.Include(d=>d.Devices)
            //.Include(d=>d.IdUserNavigation)
            //.Include(d=>d.EventsDevices)

            string username = User.Identity.Name;
            //var devices = _db.Measurements.Where(d => d.Device.User.Username == username).Select(d => d.Device).ToList();
            //var devices = _db.Devices.Where(d => d.User.Username == username).Include(d=>d.Measurements.Where(d=>d.Device.User.Username==username).Select(d=>d)).Select(s=> new {s, s.Measurements}).ToList();
            //var devices = _db.Devices
            //    .Include(d => d.Measurements).Where(d => d.User.Username == username).ToList();
            //      var deviceByUser = _db.Users.Include(d => d.Devices).Where(d => d.Username == username).ToList();
            //       var measurements = _db.Measurements.Include(d=>d.Device).Where(d => d.Device.User.Username == username);
            var devices = _db.Devices!.Where(x => x.User.Username == username).ToList();

            for (var i = 0; i < devices.Count; i++)
            {
                devices[i].Measurements = new[]
                {
                    _db.Measurements!.Where(x => x.Device == devices[i])
                        .OrderBy(x => x.Date).Last()
                };
            }
     //       var devices = _db.Devices.Include(d=>d.Measurements).FirstOrDefault(d => d.User.Username == username);
     //var devices = _db.Measurements.Where(d => d.Device.User.Username == username).OrderBy(d => d.Date).Last();
            return View(devices);
        }   

        [Authorize]
        [Route("Device/{DeviceName}")]
        public IActionResult Show(string DeviceName,string span)
        {
            if (DeviceName != "" && span != "1")
            {
                string username = User.Identity.Name;
                var device = _db.Devices.Include(d => d.Measurements.Where(d => DateTime.Now.Subtract(d.Date) < TimeSpan.FromHours(5)))
                    .FirstOrDefault(d => d.Name == DeviceName && d.User.Username == username);
                return View(device);
            }
            return NotFound();
        }

    }
}
