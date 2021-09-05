using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MQTTDashboard.Models.DbModels;

namespace MQTTWebApi.Models.ForReport
{
    public class DeviceViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Geo { get; set; }
        public string Descr { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? EditDate  { get; set; }
        public IEnumerable<MeasurementViewModel> LastTenMeasurements { get; set; } 
        public IEnumerable<EventDeviceViewModel> LastTenEvents { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        

    }
}
