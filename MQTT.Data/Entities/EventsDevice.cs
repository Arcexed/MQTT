#region

using System;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

#endregion

namespace MQTT.Data.Entities
{
    public class EventsDevice : IEntity<Guid>
    {
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public bool IsSeen { get; set; } = false;
        public Guid Id { get; set; }
    }
}