using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MQTTWebApi.Models;
using MQTTWebApi.Static;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MQTTWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        // GET: api/<DeviceController>
        [HttpGet]
        public IEnumerable<Device> Get()
        {
            using (MqttDBContext db = new MqttDBContext())
            {
                return db.Device.ToArray();
            }
        }

        // GET api/<DeviceController>/5
        [HttpGet("{id}")]
        public IEnumerable<Device> Get(string name)
        {
            using (MqttDBContext db = new MqttDBContext())
            {
                return db.Device.Where(d => d.Name.Equals(name)).ToArray();
            }
        }
        [HttpPost("Add")]
        public string Post(string name, string descr,string geo)
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
                        return "Success";
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



    }
}
