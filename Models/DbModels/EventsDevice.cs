using System;
using System.Collections.Generic;

#nullable disable

namespace Models.DbModels
{
    public partial class EventsDevice
    {
        public Guid Id { get; set; }
        public Guid IdDevice { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }

        public virtual Device IdDeviceNavigation { get; set; }
    }
}
