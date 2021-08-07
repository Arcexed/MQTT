using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MQTTWebApi.Models
{
    public class Measurements
    {
        [Key]
        public Guid Id { get; set; }
        [Key]
        public Device Device { get; set; }
        public DateTime Time { get; set; }
        public float AtmosphericPressure { get; set; }
        public float Temperature { get; set; }
        public float AirHumidity { get; set; }
        public float LightLevel { get; set; }
        public float SmokeLevel { get; set; }

    }
}
