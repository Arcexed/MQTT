using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MQTTWebApi.Models
{
    public class EventDeviceViewModel {
        public Guid Id { get; set; }
        public string Message { get; set; }
        [JsonIgnore]
        public DateTime _date { get; set; }
        public string Date => _date.ToString("G");
    }
}
