using System;
using System.Collections.Generic;

#nullable disable

namespace MQTTDashboard.Models.DbModels
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
