﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MQTTDashboard.Models.DbModels;

namespace MQTTWebApi.Models
{
    public class EventDeviceViewModel {
        public Guid Id { get; set; }
        public string Message { get; set; } 
        public DateTime Date { get; set; }
        [JsonIgnore]
        public virtual Device Device { get; set; }
    }
}
