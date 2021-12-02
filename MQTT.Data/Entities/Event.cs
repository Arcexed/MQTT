using System;
using System.ComponentModel.DataAnnotations;

namespace MQTT.Data.Entities
{
    public class Event
    {
        [Required] public DateTime Date { get; set; }
        
#pragma warning disable 8618
        [Required] public string Message { get; set; }
#pragma warning restore 8618

        public bool IsSeen { get; set; } = false;
        
        [Required] public Guid Id { get; set; }


    }
}