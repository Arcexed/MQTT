using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MQTTDashboard.Models.DbModels;

namespace MQTTDashboard.Models
{
    public class EventUserViewModel
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public Guid IdUser { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
