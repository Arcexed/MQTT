using System;
using System.Collections.Generic;

#nullable disable

namespace MQTTDashboard.Models.DbModels
{
    public partial class Device
    {
        public Device()
        {
            EventsDevice = new HashSet<EventDevice>();
            Measurements = new HashSet<Measurement>();
        }

        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public string Name { get; set; }
        public DateTime CreatingDate { get; set; }
        public DateTime? EditingDate { get; set; }
        public string Geo { get; set; }
        public string Descr { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<EventDevice> EventsDevice { get; set; }
        public virtual ICollection<Measurement> Measurements { get; set; }
    }
}
