using System;
using System.ComponentModel.DataAnnotations;

namespace MQTT.Data.Entities
{
    public class Event
    {
        [Required] public DateTime Date { get; set; }
        
        [Required] public string Message { get; set; }

        public bool IsSeen { get; set; } = false;
        
        [Required] public Guid Id { get; set; }


    }
}