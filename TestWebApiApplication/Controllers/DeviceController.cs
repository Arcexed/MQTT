using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using MQTTWebApi.Models;
using MQTTWebApi.Models.ForReport;
using System.Text.Json;
using System.Text.Json.Serialization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MQTTWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MqttdbContext _db;
        private readonly ILogger<DeviceController> _logger;

        public DeviceController(ILogger<DeviceController> logger,MqttdbContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }
        // GET: <DeviceController>  
        [HttpGet]
        public IEnumerable<DeviceViewModel> AllDevicesGET()
        {
            IEnumerable<Device> devices = _db.Devices.Include(d => d.Measurements.Take(3)).Include(d => d.Events.Take(3)).AsSingleQuery();
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
            DeviceViewModel deviceViewModel = _mapper.Map<Device, DeviceViewModel>(_db.Devices.Where(d => d.Name.Equals(name)).Include(d => d.Measurements.Take(3)).Include(d => d.Events.Take(3)).AsSingleQuery().FirstOrDefault());
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
        public string AddDeviceGET(string name, string descr, string geo)
        {
            try
            {
                var isDuplicateItem = _db.Devices.Any(d => d.Name.ToUpper() == name);

                    if (!isDuplicateItem)
                    {
                        Device device = new Device()
                        {
                            Name=name,
                            Descr=descr,
                            Geo=geo,
                            CreateDate = DateTime.Now,
                            EditDate = null
                        };
                        _db.Devices.Add(device);
                        _db.SaveChanges();
                        return "Success adding";
                    }
                    else
                    {
                        return "This device is exists";
                    }
                
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [HttpGet("Edit")]
        public string EditDeviceGET(string name, string descr, string geo)
        {
            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    var device = _db.Devices.Where(d => d.Name.ToUpper() == name).FirstOrDefault();

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
                        device.EditDate=DateTime.Now;
                        _db.SaveChanges();
                        return "Success editing";
                    }
                    else
                    {
                        Response.StatusCode = 404;
                        return "This device is not exists";
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else
            {
                Response.StatusCode = 404;
                return "Name is null or empty";
            }
        }
        [HttpGet("Delete")]
        public string DeleteDeviceGET(string name)
        {

            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    var ExistsItem = _db.Devices.Where(d => d.Name.ToUpper() == name).FirstOrDefault();
                    if (ExistsItem != null)
                    {
                        _db.Devices.Remove(ExistsItem);

                        _db.SaveChanges();
                        return "Success delete";
                    }
                    else
                    {
                        Response.StatusCode = 404;
                        return "This device is not exists";
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else
            {
                Response.StatusCode = 404;
                return "Name is null or empty";
            }
        }
    }
}
