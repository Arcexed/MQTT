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
        [Authorize]
        [HttpGet]
        public IActionResult AllDevicesForUserGet()
        {
            var user = _db.Users.Include(d=>d.Devices).First(x => x.Username == User.Identity!.Name);
            var devices = user.Devices;
            // var devices =
            //     _db.Users.FirstOrDefault(x => x.Username == User.Identity!.Name)?.Devices;
                devices.ForAll(x => x.TakeMeasurements(3).TakeEvents(3));
                var deviceView =
                    _mapper.Map<IEnumerable<Device>, IEnumerable<DeviceViewModel>>(devices!);
                return Ok(deviceView);

            
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
                var user = _db.Users.FirstOrDefault(d => d.Username == User.Identity!.Name);
                if (user != null)
                {
                    Device device = new()
                    {
                        Id = Guid.NewGuid(),
                        Name = name,
                        Description = desc,
                        Geo = geo,
                        CreatingDate = DateTime.Now,
                        EditingDate = null,
                        IsPublic = false,
                        MqttToken = Guid.NewGuid().ToString("N")
                    };
                    user.Devices.Add(device);
                    _db.SaveChanges();
                    return Ok("Success adding");
                }
                else
                {
                    return BadRequest("User not found");
                }
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