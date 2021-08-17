using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQTTWebApi.Models.ForReport
{
    public class MeasurementViewModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public double AtmosphericPressure { get; set; }
        public double Temperature { get; set; }
        public double AirHumidity { get; set; }
        public double LightLevel { get; set; }
        public double SmokeLevel { get; set; }
        
    }
}
