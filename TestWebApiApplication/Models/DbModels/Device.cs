using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

#nullable disable

namespace MQTTWebApi.Models
{
    public partial class Device
    {
        public Device()
        {
            Events = new HashSet<Event>();
            Measurements = new HashSet<Measurement>();
        }
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Geo { get; set; }
        public string Descr { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? EditDate { get; set; }

        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Measurement> Measurements { get; set; }
    }
}
