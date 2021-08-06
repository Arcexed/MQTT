using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQTTWebApi.Models
{
    public class Measurements
    {
        public string id { get; set; }
        public Device device { get; set; }
        public DateTime time { get; set; }
        public float atmospheric_pressure { get; set; }
        public float temperature { get; set; }
        public float air_humidity { get; set; }
        public float light_level { get; set; }
        public float smoke_level { get; set; }

    }
}
