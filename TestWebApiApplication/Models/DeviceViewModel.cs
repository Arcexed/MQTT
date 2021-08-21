using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MQTTWebApi.Models.ForReport
{
    public class DeviceViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Geo { get; set; }
        public string Descr { get; set; }
        [JsonIgnore]
        public DateTime _createDate { get; set; }
        public string CreateDate => _createDate.ToString("G");
        [JsonIgnore]
        public DateTime? _editDate { get; set; }
        public string EditDate => _editDate.Value.ToString("G");
        public IEnumerable<MeasurementViewModel> LastTenMeasurements { get; set; }
        public IEnumerable<EventViewModel> LastTenEvents { get; set; }
        private DateTime? _lastMeasurement
        {
            get
            {
                return LastTenMeasurements.Select(d => d._date).OrderByDescending(d => d.Date).FirstOrDefault();
            }
        }

        public string LastMeasurement => _lastMeasurement.Value.ToString("G");

    }
}