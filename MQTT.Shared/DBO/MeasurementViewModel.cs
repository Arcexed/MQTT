#region

using System;
using System.Text.Json.Serialization;
using MQTT.Data.Entities;

// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

#endregion

namespace MQTT.Shared.DBO
{
    public class MeasurementViewModel
    {
        public Guid Id { get; set; }

        [JsonIgnore] public Guid IdDevice { get; set; }

        public DateTime Date { get; set; }
        public double AtmosphericPressure { get; set; }
        public double Temperature { get; set; }
        public double AirHumidity { get; set; }
        public double LightLevel { get; set; }
        public double SmokeLevel { get; set; }
        public double RadiationLevel { get; set; }

        [JsonIgnore] public virtual Device Device { get; set; }
    }
}