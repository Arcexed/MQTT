using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using MQTTWebApi.Models;
using MQTTWebApi.Static;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MQTTWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {

        private readonly ILogger<DeviceController> _logger;

        public DeviceController(ILogger<DeviceController> logger)
        {
            _logger = logger;
        }
        // GET: <DeviceController>
        [HttpGet]
        public IEnumerable<Device> AllDevicesGET()
        {
            using (MqttDBContext db = new MqttDBContext())
            {
                return db.Device.ToArray();
            }
        }

        // GET <DeviceController>/5
        [HttpGet("{name}")]
        public IEnumerable<Device> Get(string name)
        {
            using (MqttDBContext db = new MqttDBContext())
            {
                return db.Device.Where(d => d.Name.Equals(name)).ToArray();
            }
        }
        [HttpGet("Add")]
        public string AddDeviceGET(string name, string descr,string geo)
        {
            try
            {
                using (MqttDBContext db = new MqttDBContext())
                {
                    var isDuplicateItem = db.Device.Any(d => d.Name.ToUpper() == name);
                        
                    if (!isDuplicateItem)
                    {
                        db.Device.Add(new Device(name, descr, geo));
                        db.SaveChanges();
                        return "Success adding";
                    }
                    else
                    {
                        return "This device is exists";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [HttpGet("Edit")]
        public string EditDeviceGET(string name,string descr, string geo)
        {
            try
            {
                using (MqttDBContext db = new MqttDBContext())
                {
                    var ExistsItem = db.Device.Where(d => d.Name.ToUpper() == name).FirstOrDefault();

                    if (ExistsItem!=null)
                    {
                        if (descr != null)
                        {
                            ExistsItem.Descr = descr;
                        }

                        if (geo != null)
                        {
                            ExistsItem.Geo = geo;
                        }
                        db.SaveChanges();
                        return "Success editing";
                    }
                    else
                    {
                        return "This device is not exists";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpGet("Delete")]
        public string DeleteDeviceGET(string name)
        {
            try
            {
                using (MqttDBContext db = new MqttDBContext())
                {
                    var ExistsItem = db.Device.Where(d => d.Name.ToUpper() == name).FirstOrDefault();

                    if (ExistsItem != null)
                    {
                        db.Device.Remove(ExistsItem);
                        
                        db.SaveChanges();
                        return "Success delete";
                    }
                    else
                    {
                        return "This device is not exists";
                    }
                }
            }
            catch (Exception ex)    
            {
                return ex.Message;
            }
        }
    }
}
