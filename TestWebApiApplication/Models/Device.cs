using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace MQTTWebApi.Models
{
    public class Device
    {
        public string id { get; set; }
        public string name { get; set; }
        public string geo { get; set; }
        public string descr { get; set; }

    }
}
