using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MQTT.Data;
using MQTT.Data.Entities;
using MQTT.Shared.DBO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MQTT.Api.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly MQTTDbContext _db;
        private readonly ILogger<DeviceController> _logger;
        private readonly IMapper _mapper;

        public DeviceController(ILogger<DeviceController> logger, MQTTDbContext db, IMapper mapper)
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
            var devices =
                _db.Users.FirstOrDefault(x => x.Username == User.Identity!.Name)
                    ?.Devices;
            devices.ForAll(x => x.TakeMeasurements(3).TakeEvents(3));

            var deviceView =
                _mapper.Map<IEnumerable<Device>, IEnumerable<DeviceViewModel>>(devices!);
            return deviceView;
        }


        [Authorize]
        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var username = User.Identity?.Name;
            DeviceViewModel deviceViewModel =
                _mapper.Map<Device?, DeviceViewModel>(_db.Users
                    .FirstOrDefault(x => x.Username == username)?.Devices.FirstOrDefault(x => x.Name == name)
                    ?.TakeMeasurements(3).TakeEvents(3));

            if (deviceViewModel != null)
                return Ok(deviceViewModel);

            Response.StatusCode = 404;
            return BadRequest();
        }

        [Authorize]
        [HttpPut("Add")]
        public IActionResult AddDeviceGet(string name, string desc, string geo)
        {
            try
            {
                var isDuplicateItem = _db.Devices.Any(d => d.Name == name);

                if (isDuplicateItem) return Conflict("This device is exists");

                Device device = new()
                {
                    Name = name,
                    Description = desc,
                    Geo = geo,
                    CreatingDate = DateTime.Now,
                    EditingDate = null
                };
                _db.Devices.Add(device);
                _db.SaveChanges();
                return Ok("Success adding");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpPatch("Edit")]
        public async Task<IActionResult> EditDevice(string name, string desc, string geo)
        {
            if (!string.IsNullOrEmpty(name))
                try
                {
                    var username = User.Identity?.Name;
                    var device = (await _db.Users.FirstOrDefaultAsync(x =>
                        x.Username == username))?.Devices.FirstOrDefault(x => x.Name == name);

                    if (device != null)
                    {
                        if (desc != "")
                            device.Description = desc;

                        if (geo != "")
                            device.Geo = geo;

                        device.EditingDate = DateTime.Now;

                        await _db.SaveChangesAsync();
                        return Ok("Success editing");
                    }

                    return NotFound("This device is not exists");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{ex.Message} {Request.QueryString.Value}");
                    return BadRequest(ex.Message);
                }

            return NotFound("Name is null or empty");
        }

        [Authorize]
        [HttpDelete("Delete")]
        public IActionResult DeleteDevice(string name)
        {
            if (!string.IsNullOrEmpty(name))
                try
                {
                    var username = User.Identity?.Name;
                    var existsItem = _db.Users.FirstOrDefault(x =>
                        x.Username == username)?.Devices.FirstOrDefault(x => x.Name == name);

                    if (existsItem != null)
                    {
                        _db.Devices.Remove(existsItem);
                        _db.SaveChanges();
                        return Ok("Success delete");
                    }

                    return NotFound("This device is not exists");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            return NotFound("Name is null or empty");
        }
    }
}