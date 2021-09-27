#region

using System;
using System.ComponentModel.DataAnnotations;

// ReSharper disable ClassNeverInstantiated.Global
#pragma warning disable 8618

#endregion

namespace MQTT.Data.Entities
{
    public class EventsUser : IEntity<Guid>
    {
        [Required] public DateTime Date { get; set; }
        public string Message { get; set; }
        public bool IsSeen { get; set; } = false;
        [Required] public Guid Id { get; set; }
    }
}