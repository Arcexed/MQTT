using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace MQTTWebApi.Models
{
    public class Device
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Geo { get; set; }
        public string Descr { get; set; }

        public Device(string name, string descr, string geo)
        {
           // Id = Guid.NewGuid().ToString();
            Name = name;
            Descr = descr;
            Geo = geo;
        }
        
    }
}
