using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Models.DbModels;
using Models.DBO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MQTTWebApi.Controllers
{
    [Route("[controller]")]
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
        [Authorize]
        [HttpGet]
        public IEnumerable<DeviceViewModel> AllDevicesGET()
        {

            IEnumerable<Device> devices = _db.Devices.Include(d => d.Measurements.Take(3)).Include(d => d.EventsDevices.Take(3)).AsSingleQuery();
            IEnumerable<DeviceViewModel> deviceView =
                _mapper.Map<IEnumerable<Device>, IEnumerable<DeviceViewModel>>(devices);
               // string response = JsonSerializer.Serialize(deviceView);
 //           _logger.LogInformation($"Proccessing {Request.} {Request.Path}");
            return deviceView;
        }

        // GET <DeviceController>/5
        [HttpGet("{name}")]
        public DeviceViewModel Get(string name)
        {
            DeviceViewModel deviceViewModel = _mapper.Map<Device, DeviceViewModel>(_db.Devices.Where(d => d.Name.Equals(name)).Include(d => d.Measurements.Take(3)).Include(d => d.EventsDevices.Take(3)).AsSingleQuery().FirstOrDefault());
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
        [HttpGet("Add")]
        public IActionResult AddDeviceGET(string name, string descr, string geo)
        {
            try
            {
                var isDuplicateItem = _db.Devices.Any(d => d.Name == name);

                    if (!isDuplicateItem)
                    {
                        Device device = new Device()
                        {
                            Name=name,
                            Descr=descr,
                            Geo=geo,
                            CreatingDate = DateTime.Now,
                            EditingDate = null
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


        [HttpGet("Edit")]
        public IActionResult EditDeviceGET(string name, string descr, string geo)
        {
            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    var device = _db.Devices.FirstOrDefault(d => d.Name == name);

                    if (device != null)
                    {
                        if (descr != null)
                        {
                            device.Descr = descr;
                        }

                        if (geo != null)
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
        [HttpGet("Delete")]
        public IActionResult DeleteDeviceGET(string name)
        {

            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    var ExistsItem = _db.Devices.FirstOrDefault(d => d.Name == name);
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
