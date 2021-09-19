#region

using System;
using System.Text.Json.Serialization;
using MQTT.Data.Entities;

// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

#endregion

namespace MQTT.Shared.DBO
{
    public class EventDeviceViewModel
    {
        public Guid Id { get; set; }

        [JsonIgnore] public Guid IdDevice { get; set; }

        public DateTime Date { get; set; }
        public string Message { get; set; }

        [JsonIgnore] public Device Device { get; set; }
    }
}