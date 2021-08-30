using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MQTTWebApi.Models.ForReport
{
    public class MeasurementViewModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public float AtmosphericPressure { get; set; }
        public float Temperature { get; set; }
        public float AirHumidity { get; set; }
        public float LightLevel { get; set; }
        public float SmokeLevel { get; set; }
        [JsonIgnore]
        public Device Device { get; set; }

    }
}
