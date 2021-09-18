using System;
using System.Collections.Generic;

#nullable disable

namespace Models.Models
{
    public partial class EventsDevice
    {
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public bool isSeen { get; set; } = false;
        public Device Device { get; set; }
    }
}
