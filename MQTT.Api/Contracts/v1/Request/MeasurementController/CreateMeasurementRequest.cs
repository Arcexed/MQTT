using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace MQTT.Api.Contracts.v1.Request
{
    public class CreateMeasurementRequest
    {
        public double AtmosphericPressure { get; set; }
        public double Temperature { get; set; }
        public double AirHumidity { get; set; }
        public double LightLevel { get; set; }
        public double SmokeLevel { get; set; }
        public double RadiationLevel { get; set; }
    }
}