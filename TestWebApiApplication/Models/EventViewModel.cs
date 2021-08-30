using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MQTTWebApi.Models
{
    public class EventViewModel {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}
