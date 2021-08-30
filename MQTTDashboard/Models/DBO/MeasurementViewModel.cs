using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MQTTDashboard.Models.DbModels;

namespace MQTTWebApi.Models.ForReport
{
    public class MeasurementViewModel
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public DateTime Date { get; set; }
        public float AtmosphericPressure { get; set; }
        public float Temperature { get; set; }
        public float AirHumidity { get; set; }
        public float LightLevel { get; set; }
        public float SmokeLevel { get; set; }
        [JsonIgnore]
        public virtual Device Device { get; set; }

    }
}
