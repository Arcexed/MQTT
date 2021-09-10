using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Models.Models;

namespace Models.DBO
{
    public class DeviceViewModel
    {

        public Guid Id { get; set; }
        [JsonIgnore]
        public Guid IdUser { get; set; }
        public string Name { get; set; }
        public DateTime CreatingDate { get; set; }
        public DateTime? EditingDate { get; set; }
        public string Geo { get; set; }
        public string Descr { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        public IEnumerable<MeasurementViewModel> LastThreeMeasurements { get; set; } 
        public IEnumerable<EventDeviceViewModel> LastThreeEvents { get; set; }
        

    }
}
