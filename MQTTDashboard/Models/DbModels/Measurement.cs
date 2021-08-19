using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable

namespace MQTTWebApi.Models
{
    public partial class Measurement
    {
        public Guid Id { get; set; }
        public Guid IdDevice { get; set; }
        public DateTime Date { get; set; }
        public double AtmosphericPressure { get; set; }
        public double Temperature { get; set; }
        public double AirHumidity { get; set; }
        public double LightLevel { get; set; }
        public double SmokeLevel { get; set; }
        public virtual Device Device { get; set; }
    }
}
