#region

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace MQTT.Data.Entities
{
    public class Measurement
    {
        [Required] public Guid Id { get; set; }
        [Required] public DateTime Date { get; set; }
        public double AtmosphericPressure { get; set; }
        public double Temperature { get; set; }
        public double AirHumidity { get; set; }
        public double LightLevel { get; set; }
        public double SmokeLevel { get; set; }
        public double RadiationLevel { get; set; }
        
#pragma warning disable 8618
        [Required] public Device Device { get; set; }
#pragma warning restore 8618
    }
}