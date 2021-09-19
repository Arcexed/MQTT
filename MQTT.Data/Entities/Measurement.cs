#region

using System;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

#endregion

namespace MQTT.Data.Entities
{
    public class Measurement
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public double AtmosphericPressure { get; set; }
        public double Temperature { get; set; }
        public double AirHumidity { get; set; }
        public double LightLevel { get; set; }
        public double SmokeLevel { get; set; }
        public double RadiationLevel { get; set; }
    }
}