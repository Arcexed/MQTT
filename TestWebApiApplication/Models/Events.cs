using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQTTWebApi.Models
{
    public class Events
    {
        public string id { get; set; }
        public Device id_device { get; set; }
        public string message { get; set; }
    }
}
