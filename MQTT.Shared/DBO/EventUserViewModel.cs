#region

using System;
using System.Text.Json.Serialization;
using MQTT.Data.Entities;

#endregion

namespace MQTT.Shared.DBO
{
    public class EventUserViewModel
    {
        public Guid Id { get; set; }

        [JsonIgnore] public Guid IdUser { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        [JsonIgnore] public User User { get; set; }
    }
}