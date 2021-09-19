#region

using System;

// ReSharper disable ClassNeverInstantiated.Global
#pragma warning disable 8618

#endregion

namespace MQTT.Data.Entities
{
    public class EventsUser : IEntity<Guid>
    {
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public bool IsSeen { get; set; } = false;
        public Guid Id { get; set; }
    }
}