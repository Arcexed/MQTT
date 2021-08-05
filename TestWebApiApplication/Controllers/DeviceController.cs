using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MQTTWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        // GET: api/<DeviceController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DeviceController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        
    }
}
