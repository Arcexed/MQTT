﻿using System;
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
        public DateTime _date { get; set; }
        public string Date => _date.ToString("G");
        public float AtmosphericPressure { get; set; }
        public float Temperature { get; set; }
        public float AirHumidity { get; set; }
        public float LightLevel { get; set; }
        public float SmokeLevel { get; set; }
        

    }
}