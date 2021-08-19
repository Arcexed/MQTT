using System;
using System.Collections.Generic;

#nullable disable

namespace MQTTDashboard.Models.DbModels
{
    public partial class EventDevice
    {
        public Guid Id { get; set; }
        public Guid IdDevice { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }

        public virtual Device Device { get; set; }
    }
}
