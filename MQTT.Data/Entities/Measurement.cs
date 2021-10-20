#region

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

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
        
        [Required] public Device Device { get; set; }
    }
}