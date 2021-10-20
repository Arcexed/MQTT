#region

using System;
using System.ComponentModel.DataAnnotations;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

#endregion

namespace MQTT.Data.Entities
{
    public class EventsDevice : IEntity<Guid>
    {
        [Required] public DateTime Date { get; set; }
        [Required] public string Message { get; set; }
        public bool IsSeen { get; set; } = false;
        [Required] public Guid Id { get; set; }
    }
}