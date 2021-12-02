using System;
using Microsoft.AspNetCore.Mvc;

namespace MQTT.Api.Contracts.v1.Request
{
    public class CreateMeasurementRequest
    {
        [FromRoute] public Guid DeviceId { get; set; } 
        public double AtmosphericPressure { get; set; }
        public double Temperature { get; set; }
        public double AirHumidity { get; set; }
        public double LightLevel { get; set; }
        public double SmokeLevel { get; set; }
        public double RadiationLevel { get; set; }
    }
}