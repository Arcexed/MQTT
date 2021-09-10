using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Models.Models;
using Models.DBO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MQTTWebApi.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly mqttdb_newContext _db;
        private readonly ILogger<DeviceController> _logger;

        public DeviceController(ILogger<DeviceController> logger, mqttdb_newContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }
        // GET: <DeviceController>
        //[Authorize] 
        //[Authorize(Roles="Admin, Root")]
        //[HttpGet]
        //public IEnumerable<DeviceViewModel> AllDevicesGet()
        //{

        //    IEnumerable<Device> devices = _db.Devices.Include(d => d.Measurements.Take(3)).Include(d => d.EventsDevices.Take(3));
        //    IEnumerable<DeviceViewModel> deviceView =
        //        _mapper.Map<IEnumerable<Device>, IEnumerable<DeviceViewModel>>(devices);
            
        //    return deviceView;
        //}
        [Authorize]
        [HttpGet]
        public IEnumerable<DeviceViewModel> AllDevicesForUserGet()
        {

            IEnumerable<Device> devices = _db.Devices.Include(d => d.Measurements.Take(3)).Include(d => d.EventsDevices.Take(3)).Where(d=>d.IdUserNavigation.Username==User.Identity.Name);
            IEnumerable<DeviceViewModel> deviceView =
                _mapper.Map<IEnumerable<Device>, IEnumerable<DeviceViewModel>>(devices);
               // string response = JsonSerializer.Serialize(deviceView);
 //           _logger.LogInformation($"Proccessing {Request.} {Request.Path}");
            return deviceView;
        }

        // GET <DeviceController>/5
        [Authorize]
        [HttpGet("{name}")]
        public DeviceViewModel? Get(string name)
        {
            string username = User.Identity.Name;
            DeviceViewModel deviceViewModel = 
                _mapper.Map<Device?, DeviceViewModel>(_db.Devices
                    .Include(d => d.Measurements.Take(3))
                    .Include(d => d.EventsDevices.Take(3))
                    .FirstOrDefault(d => d.Name.Equals(name) && d.IdUserNavigation.Username==username));
               // string response = JsonSerializer.Serialize(deviceViewModel);
               if (deviceViewModel != null)
               {
                   return deviceViewModel;
               }
               else
               {
                   Response.StatusCode = 404;
                   return null;
               }
        }

        [Authorize]
        [HttpGet("Add")]
        public IActionResult AddDeviceGet(string name, string descr, string geo)
        {
            try
            {
                var username = User.Identity.Name;
                var user = _db.Users.FirstOrDefault(d => d.Username == username);
                var isDuplicateItem = _db.Devices.Any(d => d.Name == name);

                    if (!isDuplicateItem)
                    {
                        Device device = new Device()
                        {
                            Name=name,
                            Descr=descr,
                            Geo=geo,
                            CreatingDate = DateTime.Now,
                            EditingDate = null,
                            IdUserNavigation = user
                        };
                        _db.Devices.Add(device);
                        _db.SaveChanges();
                        return Ok("Success adding");
                    }
                    else
                    {
                        return Conflict("This device is exists");
                    }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpGet("Edit")]
        public IActionResult EditDeviceGET(string name, string descr, string geo)
        {
            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    var username = User.Identity.Name;
                    var device = _db.Devices.FirstOrDefault(d => d.Name == name && d.IdUserNavigation.Username==username);

                    if (device != null)
                    {
                        if (descr != "")
                        {
                            device.Descr = descr;
                        }
                        if (geo != "")
                        {
                            device.Geo = geo;
                        }
                        device.EditingDate=DateTime.Now;
                        _db.SaveChanges();
                        return Ok("Success editing");
                    }
                    else
                    {
                        return NotFound("This device is not exists");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{ex.Message} {Request.QueryString.Value}");
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound("Name is null or empty");
            }
        }
        [Authorize]
        [HttpGet("Delete")]
        public IActionResult DeleteDeviceGET(string name)
        {

            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    var username = User.Identity.Name;
                    var ExistsItem = _db.Devices.FirstOrDefault(d => d.Name == name && d.IdUserNavigation.Username == username);
                    if (ExistsItem != null)
                    {

                        _db.Devices.Remove(ExistsItem);
                        _db.SaveChanges();
                        return Ok("Success delete");
                    }
                    else
                    {

                        return NotFound("This device is not exists");
                    }
                }
                catch (Exception ex)
                {
                    
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound("Name is null or empty");
            }
        }
    }
}
